using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer:Entity
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string City { get; set; } = "";
        public int ZipCode { get; set; }
        public int OrderNumber { get; set; }
        public int PhoneNumber { get; set; }
        public int Email { get; set; }
    }
}
