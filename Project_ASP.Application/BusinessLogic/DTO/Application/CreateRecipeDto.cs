using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic.DTO.Application
{
    public class CreateRecipeDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Guide { get; set; }
        public int TimeToCook { get; set; }
        public int NumOfServings { get; set; }
        public int DietId { get; set; }
        public List<IngredientRecipeDto> Ingredients { get; set; }
        public List<IFormFile> Pictures { get; set; }
    }
}
