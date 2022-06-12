using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO;
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
    public class EfCreateIngredientTypeCommand : ICreateIngredientTypeCommand
    {
        private CreateIngredientTypeValidator _validator;
        ProjectContext _context;
        public EfCreateIngredientTypeCommand(
            ProjectContext context,
            CreateIngredientTypeValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Create Ingredient Type";

        public string Description => "Add new Ingredient Type";

        public void Execute(ILookupDto request)
        {
            _validator.ValidateAndThrow(request);

            var ingredientType = new IngredientType
            {
                Name = request.Name
            };

            _context.IngredientTypes.Add(ingredientType);
            _context.SaveChanges();
            
        }
    }
}
