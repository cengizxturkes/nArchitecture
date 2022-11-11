using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; } = "";
        public string AsinCode { get; set; } = "";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Volume { get; set; }
    }
}
