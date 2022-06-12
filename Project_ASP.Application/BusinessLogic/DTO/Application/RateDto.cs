using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic.DTO.Application
{
    public class RateDto : BaseDto
    {
        public int RecipeId { get; set; }
        public int RateValue { get; set; }
    }
}
