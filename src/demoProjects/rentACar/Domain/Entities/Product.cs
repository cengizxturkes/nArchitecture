using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public double Length { get; set; }

        public string AsinCode { get; set; } = "";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Desi { get; set; }
        public decimal TotalPrice { get; set; }
        public int ExpectedTotalPrice { get; set; }
        public int ActualTotalPrice { get; set; }
        public int ExpectedStockAmount { get; set; }
        public int RecievedStockAmount { get; set; }

    }
}
