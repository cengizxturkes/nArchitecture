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
        public double RealPrice { get; set; }
        public double TotalPrice { get; set; }
        public double ExpectedTotalPrice { get; set; }
        public double ActualTotalPrice { get; set; }
        public double ExpectedStockAmount { get; set; }
        public double RecievedStockAmount { get; set; }

        //context nerde

    }
}
