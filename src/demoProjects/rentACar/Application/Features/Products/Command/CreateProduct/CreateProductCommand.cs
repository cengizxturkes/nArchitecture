using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Dtos;
using Application.Features.Products.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.CreateProduct
{
    public partial class CreateProductCommand
  : IRequest<CreatedProductDto>
    {
        public string Name { get; set; } = "";
        public int UserId { get; set; }
        public string AsinCode { get; set; } = "";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Volume { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductDto>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;


            public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;

            }

            public async Task<CreatedProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {


                Product mappedProduct = _mapper.Map<Product>(request);
                Product createdProduct = await _productRepository.AddAsync(mappedProduct);
                CreatedProductDto createdProductDto = _mapper.Map<CreatedProductDto>(createdProduct);

                return createdProductDto;

            }
        }
    }
}
