using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public int IngredientTypeId { get; set; }
        public virtual IngredientType IngredientType { get; set; }
        public virtual ICollection<IngredientRecipe> Recipes { get; set; } = new List<IngredientRecipe>();
    }
}
