using Application.Features.Brands.Dtos;
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

namespace Application.Features.Customers.Commands.CreateCustomer
{
    public partial class CreatedCustomerCommand : IRequest<CreatedCustomerDto>
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string City { get; set; } = "";
        public int ZipCode { get; set; }
        public int OrderNumber { get; set; }
        public int PhoneNumber { get; set; }
        public int Email { get; set; }

        public class CreatedCustomerCommandHandler : IRequestHandler<CreatedCustomerCommand, CreatedCustomerDto>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;


            public CreatedCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
               
            }

            public async Task<CreatedCustomerDto> Handle(CreatedCustomerCommand request, CancellationToken cancellationToken)
            {
               

                Customer mappedCustomer = _mapper.Map<Customer>(request);
                Customer createdCustomer = await _customerRepository.AddAsync(mappedCustomer);
                CreatedCustomerDto createdBrandDto = _mapper.Map<CreatedCustomerDto>(createdCustomer);

                return createdBrandDto;

            }
        }
    }
}
