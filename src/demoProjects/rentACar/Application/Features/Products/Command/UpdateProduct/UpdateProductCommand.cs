using Application.Features.Brands.Rules;
using Application.Features.Products.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdatedProductDto> 
    {
        public int Id { get; set; }
        public double ExpectedTotalPrice { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string AsinCode { get; set; } = "";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Desi { get; set; }
        public int TotalPrice { get; set; }
        public int ActualTotalPrice { get; set; }
        public int ExpectedStockAmount { get; set; }
        public int RecievedStockAmount { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductDto>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper
                                            )
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                Product mappedproduct = _mapper.Map<Product>(request);
                Product updatedProduct = await _productRepository.UpdateAsync(mappedproduct);
                UpdatedProductDto updatedProductDto = _mapper.Map<UpdatedProductDto>(updatedProduct);
                return updatedProductDto;
            }
        }
    }


}
