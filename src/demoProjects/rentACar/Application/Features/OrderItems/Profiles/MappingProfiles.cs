using Application.Features.Customers.Dtos;
using Application.Features.Customers.Models;
using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using Application.Features.Products.Command.CreateProduct;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderItems.Profiles
{
    public class MappingProfiles
  : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OrderItem, CreateOrderItemCommand>().ReverseMap();

            CreateMap<OrderItem, CreatedOrderItemDto>().ReverseMap();
            CreateMap<IPaginate<OrderItem>, OrderItemListModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemListDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemGetByIdDto>().ReverseMap();
        }
    }
}
