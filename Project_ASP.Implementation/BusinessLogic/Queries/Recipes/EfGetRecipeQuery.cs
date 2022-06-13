using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{ 
    public class EfGetRecipeQuery : IGetRecipeQuery
    {
        private readonly ProjectContext context;
        private readonly IMapper mapper;

        public int Id => 19;

        public string Name => "Get Diet Details";

        public string Description => "Get details from one recipe";

        public EfGetRecipeQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public RecipeDto Execute(int id)
        {
            var recipe = context.Recipes
                                .Include(x => x.Ingredients).ThenInclude(x => x.Ingredient)
                                .Include(x => x.Comments.Where(x=>x.EntityStatus == Domain.Enums.eEntityStatus.Active))
                                .Include(x => x.Rates.Where(x => x.EntityStatus == Domain.Enums.eEntityStatus.Active))
                                .Include(x => x.Images.Where(x => x.EntityStatus == Domain.Enums.eEntityStatus.Active)).ThenInclude(x => x.Image)
                                .Where(x => x.Id == id).FirstOrDefault();
            if (recipe == null || recipe.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Recipe),id);
            }

            if (recipe.Rates.Any())
            {
                recipe.AvgRate = (float)Math.Round((recipe.Rates.Sum(x => x.RateValue) / (decimal)recipe.Rates.Count()), 2);
            }

            return mapper.Map<RecipeDto>(recipe); 
        }
    }
}
