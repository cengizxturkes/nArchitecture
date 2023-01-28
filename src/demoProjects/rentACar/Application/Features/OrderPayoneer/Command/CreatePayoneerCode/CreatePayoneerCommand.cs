using Application.Features.OrderPayoneer.Dtos;
using Application.Features.Products.Command.CreateProduct;
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

namespace Application.Features.OrderPayoneer.Command.CreatePayoneerCode
{
    public partial class CreatePayoneerCommand:IRequest<CreatedPayoneerDto>
    {

        public int UserId { get; set; }
        public string PayonerCode { get; set; }
        public class CreatePayoneerCommandHandler : IRequestHandler<CreatePayoneerCommand, CreatedPayoneerDto>
        {
            private readonly IPayoneerRepository _payoneerRepository;
            private readonly IMapper _mapper;


            public CreatePayoneerCommandHandler(IPayoneerRepository payoneerRepository, IMapper mapper)
            {
                _payoneerRepository = payoneerRepository;
                _mapper = mapper;

            }

            public async Task<CreatedPayoneerDto> Handle(CreatePayoneerCommand request, CancellationToken cancellationToken)
            {


                Payoneer payoneer = _mapper.Map<Payoneer>(request);
                Payoneer createdpayoneer = await _payoneerRepository.AddAsync(payoneer);
                CreatedPayoneerDto createdPayoneerDto = _mapper.Map<CreatedPayoneerDto>(createdpayoneer);

                return createdPayoneerDto;

            }
        }
    }
}
