using Application.Features.Customers.Dtos;
using Application.Features.Orders.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Models
{
    public class OrderListModel
: BasePageableModel
    {
        public IList<OrderListDto> Items { get; set; }

        //
    }
}
