using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic.DTO.Application
{
    public class CommentDto : BaseDto
    {
        //public UserDto User { get; set; }
        public int UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string CommentText { get; set; }
        public int RecipeId { get; set; }
    }
}
