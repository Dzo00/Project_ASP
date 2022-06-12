using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class Recipe : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Guide { get; set; }
        public int TimeToCook { get; set; }
        public float AvgRate { get; set; }
        public int NumOfServings { get; set; }
        public int DietId { get; set; }
        public int UserId { get; set; }
        public virtual Diet Diet { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<IngredientRecipe>  Ingredients { get; set; } = new List<IngredientRecipe>();
        public virtual ICollection<RecipeImage> Images { get; set; } = new List<RecipeImage>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}
