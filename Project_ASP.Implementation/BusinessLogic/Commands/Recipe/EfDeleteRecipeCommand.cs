using Microsoft.EntityFrameworkCore;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.DataAccess.Extensions;
using Project_ASP.Domain.Entities;
using Project_ASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfDeleteRecipeCommand : IDeleteRecipeCommand
    {
        private readonly ProjectContext context;
        private readonly IApplicationUser user;

        public int Id => 21;

        public string Name => "Delete Recipe";

        public string Description => "Soft delete of recipe in the system";

        public EfDeleteRecipeCommand(ProjectContext context, IApplicationUser user)
        {
            this.context = context;
            this.user = user;
        }
        public void Execute(int request)
        {
            var recipe = context.Recipes
                                .Include(x => x.Ingredients)
                                .Include(x => x.Images).ThenInclude(x => x.Image)
                                .Include(x => x.Comments)
                                .Include(x => x.Rates)
                                .Where(x=>x.Id == request)
                                .FirstOrDefault();

            if (recipe == null || recipe.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Recipe), request);
            }

            if(recipe.UserId != user.Id)
            {
                throw new UnauthorizedException();
            }

            if (recipe.Rates.Any()){
                var rateIds = recipe.Rates.Select(x=>x.Id).ToList();
                context.SoftDelete<Rate>(rateIds);
            }
            if (recipe.Comments.Any())
            {
                var commentIds = recipe.Comments.Select(x=>x.Id).ToList();
                context.SoftDelete<Comment>(commentIds);
            }

            if (recipe.Ingredients.Any())
            {
                var ingredientIds = recipe.Ingredients.Select(x => x.Id).ToList();
                context.SoftDelete<IngredientRecipe>(ingredientIds);
            }

            if (recipe.Images.Any())
            {
                var recipeImageIds = recipe.Images.Select(x => x.Id).ToList();
                var imageIds = recipe.Images.Select(x => x.ImageId).ToList();
                context.SoftDelete<RecipeImage>(recipeImageIds);
                context.SoftDelete<Image>(imageIds);

            }

            context.SoftDelete(recipe);
            context.SaveChanges();

        }
    }
}
