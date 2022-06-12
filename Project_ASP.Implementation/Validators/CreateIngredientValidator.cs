using FluentValidation;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Validators
{
    public class CreateIngredientValidator : AbstractValidator<CreateIngredientDto>
    {
        public CreateIngredientValidator(ProjectContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required")
                .Must(x => x.Length <= 20).WithMessage("Name can have 20 charachters")
                .Must(x => !context.Ingredients.Any(y => y.Name == x))
                .WithMessage("Name {PropertyValue} already exists.");

            RuleFor(x => x.IngredientTypeId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ingredient Type is required")
                .Must(x => context.IngredientTypes.Any(y => y.Id == x))
                .WithMessage("Provided Ingredient Type does not exist.");
        }
    }
}
