using Application.Features.Products.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Amazon
{
    public interface IAmazonService
    {
        Task<CreatedProductDto> FindAsync(string asincode);
    }
}
