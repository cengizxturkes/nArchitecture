using Application.Features.Customers.Commands.CreateCustomer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandValiator
 : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValiator()
        {
            RuleFor(c => c.AsinCode).NotEmpty();
            RuleFor(c => c.Width).NotEmpty();
            RuleFor(c => c.Height).NotEmpty();
        }
    }
}
