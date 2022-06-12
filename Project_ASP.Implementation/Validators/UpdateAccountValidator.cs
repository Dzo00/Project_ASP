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
    public class UpdateAccountValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateAccountValidator(ProjectContext context)
        {
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(50).When(x => x.DisplayName != null);
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches("^(?:(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,15})$").WithMessage("Password must contain at least one number,one lowercased and one uppercased letter, and have 8-15 characters.")
                .When(x => x.Password != null);
        }
    }
}
