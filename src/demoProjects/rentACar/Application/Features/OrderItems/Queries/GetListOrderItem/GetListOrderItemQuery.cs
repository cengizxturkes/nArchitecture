using Application.Features.Customers.Models;
using Application.Features.Customers.Queries.GetListCustomer;
using Application.Features.OrderItems.Models;
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

namespace Application.Features.OrderItems.Queries.GetListOrderItem
{
    public class GetListOrderItemQuery
   : IRequest<OrderItemListModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public class GetListOrderItemQueryHandler : IRequestHandler<GetListOrderItemQuery, OrderItemListModel>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;

            public GetListOrderItemQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
            }

            public async Task<OrderItemListModel> Handle(GetListOrderItemQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OrderItem> orderItem = await _orderItemRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                OrderItemListModel orderItemListModel = _mapper.Map<OrderItemListModel>(orderItem);

                return orderItemListModel;
            }
        }
    }
}
