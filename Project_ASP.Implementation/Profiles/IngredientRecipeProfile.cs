using AutoMapper;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Profiles
{
    internal class IngredientRecipeProfile : Profile
    {
        public IngredientRecipeProfile()
        {
            CreateMap<IngredientRecipeDto, IngredientRecipe>();
            CreateMap<IngredientRecipe, IngredientRecipeDto>()
                .ForMember(m => m.IngredientName, opt => opt.MapFrom(x => x.Ingredient.Name))
                .ForMember(m => m.MeasureName, opt => opt.MapFrom(x => x.Measure));

        }
    }
}
