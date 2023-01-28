using Application.Features.Orders.Dtos;
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
    public class ProductRepository : EfRepositoryBase<Product, BaseDbContext>, IProductRepository
    {
        public ProductRepository(BaseDbContext context) : base(context)
        {
        }

        public OrderViewModel GetOrderbyCode(string code)
        {
            var product = Context.Products.Where(x => x.OrderCode == code).ToList();
            OrderViewModel order = new OrderViewModel();
            order.OrderCode = code;
            order.OrderDate = product.First().OrderDate;
            order.Products = product;
            
            return (order);
        }

      
        public List<OrderViewModel> GetOrderbyUser(int userId)
        {
            var product = Context.Products.Where(x => x.UserId == userId&&x.IsOrder==true).ToList();
            List<OrderViewModel> orders = new List<OrderViewModel>();
            foreach(var item in product)
            {
                if (orders.Where(x => x.OrderCode == item.OrderCode).Any())
                    continue;
                orders.Add(GetOrderbyCode(item.OrderCode));

            }
            return orders;
        }
    }
}
