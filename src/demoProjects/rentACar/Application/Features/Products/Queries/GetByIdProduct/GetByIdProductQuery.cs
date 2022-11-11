using Application.Features.Customers.Dtos;
using Application.Features.Customers.Queries.GetByIdCustomer;
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

namespace Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery
    : IRequest<ProductGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductGetByIdDto>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ProductGetByIdDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                Product? product = await _productRepository.GetAsync(b => b.Id == request.Id);


                ProductGetByIdDto productGetByIdDto = _mapper.Map<ProductGetByIdDto>(product);
                return productGetByIdDto;
            }
        }
    }
}
