﻿using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CustomerRepository : EfRepositoryBase<Customer, BaseDbContext>, ICustomerRepository
    {
        public CustomerRepository(BaseDbContext context) : base(context)
        {
            
        }

    }
}
