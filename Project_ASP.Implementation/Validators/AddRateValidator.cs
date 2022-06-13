using FluentValidation;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Validators
{
    public class AddRateValidator : AbstractValidator<CreateRateDto>
    {
        public AddRateValidator(ProjectContext context)
        {
            RuleFor(x => x.RecipeId).NotNull().WithMessage("Recipe is requred").Must(x => context.Recipes.Any(y => y.Id == x)).WithMessage("Provided recipe is not in the system.");
            RuleFor(x => x.RateValue).NotNull().WithMessage("Rate value is requred").Must(x => x > 0 && x < 6).WithMessage("Rate value can be 1 - 5");
        }
    }
}
