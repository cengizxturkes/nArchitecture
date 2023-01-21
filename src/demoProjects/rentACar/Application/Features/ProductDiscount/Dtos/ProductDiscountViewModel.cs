using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductDiscount.Dtos
{
    public class ProductDiscountViewModel
    {

        public bool added { get; set; }
        public int DiscountID { get; set; }

        public Discount?  Discount { get; set; }
        public int ProductDiscountID { get; set; }

    }
}
