using FluentValidation;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Validators
{
    public class UpdateRecipeValidator : AbstractValidator<UpdateRecipeDto>
    {
        public UpdateRecipeValidator(ProjectContext context, IApplicationUser applicationUser)
        {
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(60).WithMessage("Max number of characters is 60")
                .Must(x => !context.Recipes.Any(y => y.UserId == applicationUser.Id && y.Title == x)).WithMessage("You already have a recipe with this Title");
            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Max number of characters is 200");
            RuleFor(x => x.Guide)
                .Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Guide is required");
            RuleFor(x => x.TimeToCook)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Time To Cook is required")
                .Must(x => x > 0).WithMessage("You can't enter a negative value or 0");
            RuleFor(x => x.NumOfServings)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Number of servings is required")
                .Must(x => x > 0).WithMessage("You can't enter a negative value or 0");
            RuleFor(x => x.DietId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Diet is required")
                .Must(x => context.Diets.Any(y => y.Id == x)).WithMessage("Diet does not exist in the system");
            RuleFor(x => x.Ingredients)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ingredients are required!")
                .Must(x =>
                {
                    var ingridientIds = x.Select(y => y.IngredientId);
                    return ingridientIds.Distinct().Count() == ingridientIds.Count();
                }).WithMessage("Duplicates for ingredients are not allowed")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.Ingredients)
                       .Cascade(CascadeMode.Stop)
                       .Must(x => context.Ingredients.Any(y => y.Id == x.IngredientId)).WithMessage("Selected Ingredient does not exist in the system")
                       .Must(x => x.Quantity > 0).WithMessage("Quantity is requied")
                       .ChildRules( child => child.RuleFor(x=>x.MeasureId).NotNull().WithMessage("Measure is requied"));
                });

            RuleFor(x => x.NewPictures)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Pictures are required!")
                .When(x => x.ExistingPictures != null && x.ExistingPictures.Count() == 0).WithMessage("If old pictures are deleted, you must provide at least one new picture")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.NewPictures)
                       .Cascade(CascadeMode.Stop)
                       .Must(x =>
                       {
                           var allowedFormat = new List<string> { "image/jpeg", "image/png", "image/jpg" };

                           return allowedFormat.Contains(x.ContentType);
                       }).WithMessage("Allowed formats are 'image/jpeg','image/jpg' and 'image/png'");
                });
            RuleFor(x => x.ExistingPictures).Cascade(CascadeMode.Stop).Must(x =>
            {
                var ids = x.Select(x => x.Image.Id).ToList();
                return ids.Distinct().Count() == x.Count();
            }).WithMessage("Recipe does not have two same pictures, please fix your input")
            .DependentRules(() =>
            {
                RuleForEach(x => x.ExistingPictures).Must(x => context.Images.Any(y => y.Id == x.Image.Id)).WithMessage("Provided image is not in the system").When(x => x.ExistingPictures != null && x.ExistingPictures.Count() != 0);
            })
            .When(x => x.ExistingPictures != null && x.ExistingPictures.Count() != 0)
            .NotNull().WithMessage("Null is not allowed, send array: ExistingPictures[0] - empty array");
        }
    }
}
