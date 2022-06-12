using Project_ASP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class IngredientRecipe : Entity
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public string Quantity { get; set; } // zbog vrednosti kao sto je 1/8 solje itd.
        public eMeasure MeasureId { get; set; }
        public Ingredient Ingredient { get; set; }
        public Recipe Recipe { get; set; }
        public eMeasure Measure { get; set; }
    }
}
