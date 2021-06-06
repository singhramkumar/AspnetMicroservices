using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Extenstions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, 
            Action<TContext, IServiceProvider> seeder , int? retry=0) where TContext: DbContext
        {
            int retryForAvailablility = retry.Value;

            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {

                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext));
                    InvokeSeeder(seeder, context, services);
                }
                catch(SqlException ex)
                {
                    logger.LogError(ex, "An error occured while migrating database associated with context {DbContextName}", typeof(TContext));

                    if(retryForAvailablility < 50)
                    {
                        retryForAvailablility++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailablility);
                    }
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
