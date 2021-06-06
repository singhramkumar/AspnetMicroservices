﻿using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext): base(dbContext)
        {

        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            return await this._dbContext.Orders.Where(c => c.UserName == userName).ToListAsync();

        }
    }
}
