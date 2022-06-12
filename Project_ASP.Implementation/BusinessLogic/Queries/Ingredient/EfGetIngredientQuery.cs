using AutoMapper;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_ASP.Domain.Entities;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfGetIngredientQuery : IGetIngredientQuery
    {
        private readonly ProjectContext context;
        private readonly IMapper mapper;

        public int Id => 14;

        public string Name => "Get Ingredient";

        public string Description => "Get single ingredient details";

        public EfGetIngredientQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IngredientDto Execute(int search)
        {
            var ingredient = context.Ingredients.Find(search);
            if (ingredient == null || ingredient.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Ingredient), search);
            }

            return mapper.Map<IngredientDto>(ingredient);
        }
    }
}
