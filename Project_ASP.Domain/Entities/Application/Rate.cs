using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class Rate : Entity
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public int RateValue { get; set; }
        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
