using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Dtos;
using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.Orders.Dtos;
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

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand
   : IRequest<CreatedOrderDto>
    {

        public string OrderNumber { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public int DocStatus { get; set; } = 0; //0 iptal, 1 onay bekliyor, 2 kargolandı, 3 teslim edildi
        public int TotalPrice { get; set; }
        public double TotalAmount { get; set; }
        public int CustomerID { get; set; }
        public virtual IEnumerable<CreateOrderItemCommand> OrderItems { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;


            public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IOrderItemRepository orderItemRepository)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
                _orderItemRepository = orderItemRepository;
            }

            public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                //order gözükmüyor.

                Order mappedOrder = _mapper.Map<Order>(request);
                Order createdOrder = await _orderRepository.AddAsync(mappedOrder);
                CreatedOrderDto createdOrderDto = _mapper.Map<CreatedOrderDto>(createdOrder);

                foreach (var item in request.OrderItems)
                {
                    item.OrderID = createdOrder.Id;
                    OrderItem mappedOrderItem = _mapper.Map<OrderItem>(item);
                    OrderItem createdOrderItem = await _orderItemRepository.AddAsync(mappedOrderItem);
                }
                return createdOrderDto;

            }
        }
    }
}
