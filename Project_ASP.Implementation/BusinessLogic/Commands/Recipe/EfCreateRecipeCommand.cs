using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using Project_ASP.Domain.Enums;
using Project_ASP.Domain.Interfaces;
using Project_ASP.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfCreateRecipeCommand : ICreateRecipeCommand
    {
        private readonly ProjectContext context;
        private readonly CreateRecipeValidator validator;
        private readonly IApplicationUser user;

        public int Id => 17;

        public string Name => "Add Recipe";

        public string Description => "Add new recipe";
        public EfCreateRecipeCommand(ProjectContext context,CreateRecipeValidator validator, IApplicationUser user)
        {
            this.context = context;
            this.validator = validator;
            this.user = user;
        }
        public void Execute(CreateRecipeDto request)
        {
            validator.ValidateAndThrow(request);

            var images = new List<Image>();

            request.Pictures.ForEach(x => {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(x.FileName);

                var newFileName = guid + "_" + Path.GetFileNameWithoutExtension(x.FileName) + extension;

                var path = Path.Combine("wwwroot", "images", newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    x.CopyTo(fileStream);
                }

                images.Add(new Image { Path = newFileName, Alt = Path.GetFileNameWithoutExtension(x.FileName) });
            });


            var recipe = new Recipe
            {
                Title = request.Title,
                Description = request.Description,
                Guide = request.Guide,
                TimeToCook = request.TimeToCook,
                NumOfServings = request.NumOfServings,
                DietId = request.DietId,
                UserId = user.Id,
                AvgRate = 0,
                Ingredients = request.Ingredients.Select(x=> new IngredientRecipe
                {
                    IngredientId = x.IngredientId,
                    MeasureId = x.MeasureId.Value,
                    Quantity = $"{x.Quantity}"
                }).ToList(),
                Images = images.Select(x=> new RecipeImage
                {
                    Image = x
                }).ToList()

            };

            context.Recipes.Add(recipe);
            context.SaveChanges();
        }
    }
}
