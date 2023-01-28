using Application.Features.OrderPayoneer.Command.CreatePayoneerCode;
using Application.Features.OrderPayoneer.Dtos;
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

namespace Application.Features.OrderPayoneer.Profiles
{
    public class MappingProfiles
: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Payoneer, CreatedPayoneerDto>().ReverseMap();
            CreateMap<Payoneer, CreatePayoneerCommand>().ReverseMap();


        }
    }
}
