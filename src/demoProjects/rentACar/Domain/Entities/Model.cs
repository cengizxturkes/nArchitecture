using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Model : Entity
    {
        public string Name { get; set; }
    }
    public class Brand : Entity
    {
        public string Name { get; set; }

    }
}
