using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderPayoneer.Dtos
{
    public class CreatedPayoneerDto
    {

        public int UserId { get; set; }
        public string PayonerCode { get; set; }
    }
}
