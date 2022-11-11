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

namespace Application.Features.Orders.Queries.GetByIdOrder
{
    public class GetByIdOrderQuery
  : IRequest<OrderGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdOrderQueryQueryHandler : IRequestHandler<GetByIdOrderQuery, OrderGetByIdDto>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;

            public GetByIdOrderQueryQueryHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<OrderGetByIdDto> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
            {
                Order? order = await _orderRepository.GetAsync(b => b.Id == request.Id);


                OrderGetByIdDto orderGetByIdDto = _mapper.Map<OrderGetByIdDto>(order);
                return orderGetByIdDto;
            }
        }
    }
}
