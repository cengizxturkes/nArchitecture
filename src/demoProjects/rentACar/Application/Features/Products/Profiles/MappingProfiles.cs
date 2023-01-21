using Application.Features.Customers.Dtos;
using Application.Features.Customers.Models;
using Application.Features.Products.Command.CreateProduct;
using Application.Features.Products.Command.UpdateProduct;
using Application.Features.Products.Dtos;
using Application.Features.Products.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Profiles
{
    public class MappingProfiles
 : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, CreatedProductDto>().ReverseMap();
            CreateMap<IPaginate<Product>, ProductListModel>().ReverseMap();
            CreateMap<Product, ProductGetByIdDto>().ReverseMap();
            CreateMap<Product, UpdatedProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
        }
    }
}
