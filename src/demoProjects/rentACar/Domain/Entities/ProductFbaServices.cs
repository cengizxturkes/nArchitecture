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
    public class ProductFbaServices:Entity
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public decimal GoodsAcceptance { get; set; }
        public decimal LabelDisassembly { get; set; }
        public decimal FnskuLabeling { get; set; }
        public decimal AdditionalLabeling { get; set; }
        public decimal PolyBagging { get; set; }
        public decimal BubbleWrapping { get; set; }
        public decimal Boxing { get; set; }
        public decimal ReBoxing { get; set; }
        public decimal Multipack { get; set; }
        public decimal Shipping { get; set; }
        public decimal FbaBoxLabeling { get; set; }
        public decimal PaletInOut { get; set; }
        public decimal Palletizing { get; set; }
        public decimal Bundle { get; set; }


    }
}
