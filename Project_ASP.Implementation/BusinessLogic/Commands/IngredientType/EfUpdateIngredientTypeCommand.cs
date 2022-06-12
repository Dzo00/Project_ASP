using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using Project_ASP.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfUpdateIngredientTypeCommand : IUpdateIngredientTypeCommand
    {
        private readonly ProjectContext context;
        private readonly CreateIngredientTypeValidator validator;

        public int Id => 10;

        public string Name => "Update Ingredient Type";

        public string Description => "Update data for Ingredient Type";

        public EfUpdateIngredientTypeCommand(ProjectContext context, CreateIngredientTypeValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }
        public void Execute(ILookupDto request)
        {
            var ingredientType = context.IngredientTypes.Find(request.Id);
            if(ingredientType == null || ingredientType.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(IngredientType),request.Id);
            }

            validator.ValidateAndThrow(request);

            ingredientType.Name = request.Name;
            context.SaveChanges();
        }
    }
}
