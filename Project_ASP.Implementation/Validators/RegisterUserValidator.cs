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
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(ProjectContext context)
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(20).WithMessage("Lenght can not exceed 20 characters.");
            
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(20).WithMessage("Lenght can not exceed 30 characters.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.")
                .Matches("^(?:(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,15})$").WithMessage("Password must contain at least one number,one lowercased and one uppercased letter, and have 8-15 characters.");


            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is in invalid format.")
                .Must(x=> !context.Users.Any(y => y.Email == x)).WithMessage("This email is already occupied.");
        }
    }
}
