using Application.Features.Customers.Dtos;
using Application.Features.Customers.Queries.GetByIdCustomer;
using Application.Features.Orders.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderItems.Queries.GetByIdOrderItem
{
    public class GetByIdOrderItemQuery
   : IRequest<OrderGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdOrderItemQueryHandler : IRequestHandler<GetByIdOrderItemQuery, OrderGetByIdDto>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;

            public GetByIdOrderItemQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
            }

            public async Task<OrderGetByIdDto> Handle(GetByIdOrderItemQuery request, CancellationToken cancellationToken)
            {
                OrderItem? orderItem = await _orderItemRepository.GetAsync(b => b.Id == request.Id);


                OrderGetByIdDto orderGetByIdDto = _mapper.Map<OrderGetByIdDto>(orderItem);
                return orderGetByIdDto;
            }
        }
    }
}
