using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class IngredientType : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
