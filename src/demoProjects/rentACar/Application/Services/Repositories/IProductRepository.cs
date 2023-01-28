using Application.Features.Orders.Dtos;
using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>, IRepository<Product>
    {
        public OrderViewModel GetOrderbyCode(string code);
        public List<OrderViewModel> GetOrderbyUser(int userId);

    }
}
