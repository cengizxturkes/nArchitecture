using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductDiscount : Entity
    {
        public double Amount { get; set; }
        public int IDProduct { get; set; }

        [ForeignKey("IDProduct ")]
        public Product product { get; set; }

        public int discountId { get; set; }
        public Discount discount { get; set; }
        //yok mk multiplayer
        //mk askerde bile laptop getirip sana kodda ardım ettim ya ölene kadar unuurmam :D :D: :D:D:D:D
    }

    public class Discount : Entity
    {
        public double Disc { get; set; }
        public string Name { get; set; }

        public bool Multiplier { get; set; }
    }
}
