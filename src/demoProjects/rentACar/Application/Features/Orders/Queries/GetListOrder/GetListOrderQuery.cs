using Application.Features.Customers.Models;
using Application.Features.Customers.Queries.GetListCustomer;
using Application.Features.Orders.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetListOrder
{
    public class GetListOrderQuery
    : IRequest<OrderListModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public class GetListOrderQueryHandler : IRequestHandler<GetListOrderQuery, OrderListModel>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public GetListOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<OrderListModel> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Order> order = await _orderRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                OrderListModel orderListModel = _mapper.Map<OrderListModel>(order);

                return orderListModel;
            }
        }
    }
}
