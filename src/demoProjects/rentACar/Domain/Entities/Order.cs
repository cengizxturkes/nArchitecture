using Core.Persistence.Repositories;
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
        public DateTime OrderDate { get; set; }
        public int DocStatus { get; set; } = 0; //0 iptal, 1 onay bekliyor, 2 kargolandı, 3 teslim edildi
        public int TotalPrice { get; set; }
        public double TotalAmount { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
