using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payoneer:Entity
    {
        [ForeignKey("UserId")]

        public User user { get; set; }
        public string PayonerCode { get; set; }
    }
}
