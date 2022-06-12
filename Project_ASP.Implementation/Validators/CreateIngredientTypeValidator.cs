using FluentValidation;
using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Validators
{
    public class CreateIngredientTypeValidator : AbstractValidator<ILookupDto>
    {
        public CreateIngredientTypeValidator(ProjectContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required")
                .Must(x =>  x.Length <= 35 ).WithMessage("Name can have 20 charachters")
                .Must(x => !context.IngredientTypes.Any(y => y.Name == x))
                .WithMessage("Name {PropertyValue} already exists.");
    }
}
}
