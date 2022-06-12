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
    public class EfUpdateIngredientCommand : IUpdateIngredientCommand
    {
        private readonly ProjectContext context;
        private readonly UpdateIngredientValidator validator;

        public int Id => 15;

        public string Name => "Update Ingredient";

        public string Description => "Update data for Ingredient";

        public EfUpdateIngredientCommand(ProjectContext context, UpdateIngredientValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }
        public void Execute(CreateIngredientDto request)
        {
            var ingredient = context.Ingredients.Find(request.Id);
            if(ingredient == null || ingredient.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Ingredient),request.Id);
            }

            validator.ValidateAndThrow(request);

            ingredient.Name = request.Name;
            if (request.IngredientTypeId.HasValue) {
                ingredient.IngredientTypeId = request.IngredientTypeId.Value;
            }
            context.SaveChanges();
        }
    }
}
