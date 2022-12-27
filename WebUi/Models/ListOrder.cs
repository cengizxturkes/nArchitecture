using Domain.Entities;
using Persistence.Contexts;

namespace WebUi.Models
{
    public class ListOrder
    {
        private readonly BaseDbContext _context;

        

        public ListOrder(BaseDbContext context)
        {
            _context = context;
        }
        public List<Order> ListAllOrder()
        {
            return _context.Orders.ToList();

        }
    }
}
