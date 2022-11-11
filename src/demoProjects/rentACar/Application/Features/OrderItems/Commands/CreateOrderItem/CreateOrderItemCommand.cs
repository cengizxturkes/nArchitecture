using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Dtos;
using Application.Features.OrderItems.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommand
 : IRequest<CreatedOrderItemDto>
    {

        public int ProductID { get; set; } 
        public double Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string PdfOne { get; set; } = "";
        public string PdfTwo { get; set; } = "";
        public int OrderID { get; set; } 

        public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CreatedOrderItemDto>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;


            public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;

            }

            public async Task<CreatedOrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
            {


                OrderItem orderItem = _mapper.Map<OrderItem>(request);
                OrderItem createdOrderItem = await _orderItemRepository.AddAsync(orderItem);
                CreatedOrderItemDto createdOrderItemDto = _mapper.Map<CreatedOrderItemDto>(createdOrderItem);

                return createdOrderItemDto;

            }
        }
    }
}
