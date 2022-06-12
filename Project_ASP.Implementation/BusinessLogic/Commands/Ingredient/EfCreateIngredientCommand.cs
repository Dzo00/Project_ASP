using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
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
    public class EfCreateIngredientCommand : ICreateIngredientCommand
    {
        private readonly CreateIngredientValidator _validator;
        private readonly ProjectContext _context;
        public EfCreateIngredientCommand(
            ProjectContext context,
            CreateIngredientValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 12;

        public string Name => "Create Ingredient";

        public string Description => "Add new Ingredient";

        public void Execute(CreateIngredientDto request)
        {
            _validator.ValidateAndThrow(request);

            var ingredient = new Ingredient
            {
                Name = request.Name,
                IngredientTypeId = request.IngredientTypeId.Value
            };

            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
            
        }
    }
}
