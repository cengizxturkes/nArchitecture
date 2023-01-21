using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductDiscountRepository : EfRepositoryBase<ProductDiscount, BaseDbContext>, IProductDiscountRepository
    {
        public ProductDiscountRepository(BaseDbContext context) : base(context)
        {
        }
        // yok kanka discouunt da yap eter
    }
}
