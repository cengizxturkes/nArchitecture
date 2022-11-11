using Application.Features.Customers.Dtos;
using Application.Features.Customers.Models;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Products.Command.CreateProduct;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Profiles
{
    public class MappingProfiles
  : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Order, CreateOrderCommand>().ReverseMap();

            CreateMap<Order, CreatedOrderDto>().ReverseMap();
            CreateMap<IPaginate<Order>, OrderListModel>().ReverseMap();
            CreateMap<Order, OrderListDto>().ReverseMap();
            CreateMap<Order, OrderGetByIdDto>().ReverseMap();
        }
    }
}
