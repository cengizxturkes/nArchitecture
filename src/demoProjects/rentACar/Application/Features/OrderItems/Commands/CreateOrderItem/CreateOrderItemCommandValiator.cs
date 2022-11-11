using Application.Features.Customers.Commands.CreateCustomer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandValiator
   : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemCommandValiator()
        {
            RuleFor(c => c.PdfTwo).NotEmpty();
        }
    }
}
