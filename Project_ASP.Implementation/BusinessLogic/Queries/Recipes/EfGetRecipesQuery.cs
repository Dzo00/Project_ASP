using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.DTO.Search;
using Project_ASP.Application.DTO.Wrapper;
using Project_ASP.Application.Extensions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Linq;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfGetRecipesQuery : IGetRecipesQuery
    {
        public int Id => 18;

        public string Name => "Get Diets";

        public string Description => "Display all diet details";

        private ProjectContext context { get; set; }
        private IMapper mapper { get; set; }

        public EfGetRecipesQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PagingResult<RecipeDto> Execute(PagedSearch search)
        {
            var query = context.Recipes
                               .Include(x => x.Ingredients).ThenInclude(x => x.Ingredient)
                               .Include(x => x.Comments)
                               .Include(x => x.Rates)
                               .Include(x => x.Images).ThenInclude(x => x.Image)
                               .Where(x => x.EntityStatus == Domain.Enums.eEntityStatus.Active);


            if (!string.IsNullOrEmpty(search.Keyword)) {
                var keyword = search.Keyword.ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(keyword) 
                                      || x.Description.ToLower().Contains(keyword)
                                      || x.Guide.ToLower().Contains(keyword)
                                      || x.Ingredients.Any(y => y.Ingredient.Name.ToLower().Contains(keyword)));
            }

            var result = query.PagedSearch<RecipeDto, Recipe>(search, mapper);
            foreach(var d in result.Data)
            {
                if (d.Rates.Any())
                {
                    d.AvgRate = (float)Math.Round((d.Rates.Sum(x => x.RateValue) / (decimal)d.Rates.Count()), 2);
                }
            }

            return result;
        }
    }
}
