using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Dtos;
using Application.Features.Customers.Models;
using Application.Features.Products.Command.CreateProduct;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CreatedCustomerCommand>().ReverseMap();

            CreateMap<Customer, CreatedCustomerDto>().ReverseMap();
            CreateMap<IPaginate<Customer>, CustomerListModel>().ReverseMap();
            CreateMap<Customer, CustomerListDto>().ReverseMap();
            CreateMap<Customer, CustomerGetByIdDto>().ReverseMap();
        }
    }
}