using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.BusinessLogic
{
    public interface IGetRecipeQuery : IBaseQuery<int,RecipeDto>
    {
    }
}
