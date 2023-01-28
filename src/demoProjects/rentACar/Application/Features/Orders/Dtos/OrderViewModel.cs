using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Dtos
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public string OrderCode { get; set; }
      public  List<Product> Products;
    }
}
