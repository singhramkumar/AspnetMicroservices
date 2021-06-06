using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderedList
{
    public class GetOrderedListQueryHandler : IRequestHandler<GetOrderedListQuery, List<OrdersVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderedListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;

        }
        public async Task<List<OrdersVm>> Handle(GetOrderedListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderByUserName(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orders);
        }
    }
}
