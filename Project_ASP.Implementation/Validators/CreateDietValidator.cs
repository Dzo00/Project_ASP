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
    // Koristice se i za update
    public class CreateDietValidator : AbstractValidator<ILookupDto>
    {
        public CreateDietValidator(ProjectContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required")
                .Must(x =>  x.Length <= 20 ).WithMessage("Name can have 20 charachters")
                .Must( x => !context.Diets.Any(y => y.Name == x))
                .WithMessage("Name {PropertyValue} already exists.");
        }
    }
}
