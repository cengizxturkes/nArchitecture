using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Dtos
{
    public class CreatedProductDto
    {
        public string Name { get; set; } = "";
        public string AsinCode { get; set; } = "";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Volume { get; set; }
        public double Capacity { get; set; }
        public string ImageUrl { get; set; }
        public double Lenght { get; set; }
    }
}
