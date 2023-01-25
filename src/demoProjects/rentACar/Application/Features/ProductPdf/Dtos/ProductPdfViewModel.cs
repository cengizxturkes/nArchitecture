using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductPdf.Dtos
{
    public class ProductPdfViewModel
    {
        public int ProductId { get; set; }
        public IEnumerable<CustomFile> Files { get; set; }

    }
    public class CustomFile
    {
        public string File { get; set; }
        public string Name { get; set; }
    }
}
