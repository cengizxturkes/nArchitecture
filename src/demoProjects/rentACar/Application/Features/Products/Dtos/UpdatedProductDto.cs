using Core.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Dtos
{
    public class UpdatedProductDto
    : IDto
    {
        public int Id { get; set; }
        public double ExpectedTotalPrice { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string AsinCode { get; set; } = "";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Desi { get; set; }
        public int TotalPrice { get; set; }
        public int ActualTotalPrice { get; set; }
        public int ExpectedStockAmount { get; set; }
        public int RecievedStockAmount { get; set; }

    }
}
