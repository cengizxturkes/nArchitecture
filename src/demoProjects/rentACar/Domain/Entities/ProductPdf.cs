using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductPdf:Entity
    {
        
        [ForeignKey("IDProduct")]

        public Product product { get; set; }
        public int IDProduct { get; set; }
        public string FilePath { get; set; }
    }
}
