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
    public class EfGetIngredientTypeQuery : IGetIngredientTypeQuery
    {
        private readonly ProjectContext context;
        private readonly IMapper mapper;

        public int Id => 9;

        public string Name => "Get Ingredient Type";

        public string Description => "Get single ingredient type details";

        public EfGetIngredientTypeQuery(ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IngredientTypeDto Execute(int search)
        {
            var ingType = context.IngredientTypes.Find(search);
            if (ingType == null || ingType.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(IngredientType), search);
            }

            return mapper.Map<IngredientTypeDto>(ingType);
        }
    }
}
