using Application.Features.Customers.Dtos;
using Application.Features.OrderItems.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderItems.Models
{
    public class OrderItemListModel
    : BasePageableModel
    {
        public IList<OrderItemListDto> Items { get; set; }

        //
    }
}
