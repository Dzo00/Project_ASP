using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class Comment : Entity
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string CommentText { get; set; }
        public virtual User User { get; set; }  
        public virtual Recipe Recipe { get; set; }
    }
}
