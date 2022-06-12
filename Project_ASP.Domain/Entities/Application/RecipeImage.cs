using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class RecipeImage : Entity
    {
        public int ImageId { get; set; }
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Image Image { get; set; }
    }
}
