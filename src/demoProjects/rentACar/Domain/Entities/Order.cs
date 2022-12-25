using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public string OrderNumber { get; set; } = "";
        public string OrderName { get; set; }   
        public DateTime OrderDate { get; set; }
        public int DocStatus { get; set; } = 0; //0 iptal, 1 onay bekliyor, 2 kargolandı, 3 teslim edildi
        public int TotalPrice { get; set; }
        public int Price { get; set; }
        public double TotalAmount { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
