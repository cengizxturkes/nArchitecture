using Application.Features.Brands.Commands.CreateBrand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreatedCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.City).MinimumLength(2);
        }
    }
}
