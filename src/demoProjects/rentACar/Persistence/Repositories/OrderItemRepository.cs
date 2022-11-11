﻿using Application.Services.Repositories;
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
     public class OrderItemRepository : EfRepositoryBase<OrderItem, BaseDbContext>, IOrderItemRepository
    {
        public OrderItemRepository(BaseDbContext context) : base(context)
        {
        }

    }
}
