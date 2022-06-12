using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic.DTO.Application
{
    public class UserDetailsDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplatName { get; set; }
        public string Email { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<RateDto> Rates { get; set; }
        public List<RecipeDto> Recipes { get; set; }
    }
}
