using AutoMapper;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.DTO.Search;
using Project_ASP.Application.DTO.Wrapper;
using Project_ASP.Application.Extensions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfGetIngredientsQuery : IGetIngredientsQuery
    {
        private readonly ProjectContext context;
        private readonly IMapper mapper;

        public int Id => 13;

        public string Name => "Get Ingredient";

        public string Description => "Display all Ingredient details";

        public EfGetIngredientsQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PagingResult<IngredientDto> Execute(PagedSearch search)
        {
            var query = context.Ingredients.Include(x => x.IngredientType).Where(x => x.EntityStatus == Domain.Enums.eEntityStatus.Active);


            if (!string.IsNullOrEmpty(search.Keyword))
            {
                var keyword = search.Keyword.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            return query.PagedSearch<IngredientDto, Ingredient>(search, mapper);
        }
    }
}
