using Application.Features.Brands.Dtos;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Rules;
using Application.Features.Customers.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetByIdCustomer
{
    public class GetByIdCustomerQuery
   : IRequest<CustomerGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, CustomerGetByIdDto>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public GetByIdCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<CustomerGetByIdDto> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
            {
                Customer? customer = await _customerRepository.GetAsync(b => b.Id == request.Id);


                CustomerGetByIdDto customerGetByIdDto = _mapper.Map<CustomerGetByIdDto>(customer);
                return customerGetByIdDto;
            }
        }
    }
}
