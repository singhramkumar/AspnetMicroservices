using System;
using System.Collections.Generic;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderedList
{
   public class GetOrderedListQuery : IRequest<List<OrdersVm>>
    {
        public string UserName { get; set; }
        public GetOrderedListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
