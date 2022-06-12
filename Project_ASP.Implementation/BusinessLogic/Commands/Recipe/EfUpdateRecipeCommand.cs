using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
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
    public class EfUpdateRecipeCommand : IUpdateRecipeCommand
    {
        private readonly ProjectContext context;
        private readonly UpdateRecipeValidator validator;
        private readonly IApplicationUser user;

        public int Id => 20;

        public string Name => "Update Recipe";

        public string Description => "Update data for recipe";

        public EfUpdateRecipeCommand(ProjectContext context, UpdateRecipeValidator validator, IApplicationUser user)
        {
            this.context = context;
            this.validator = validator;
            this.user = user;
        }
        public void Execute(UpdateRecipeDto request)
        {
            var recipe = context.Recipes
                         .Include(x=>x.Images).ThenInclude(x=>x.Image)
                         .Include(x=>x.Ingredients)
                         .FirstOrDefault(x=> x.Id == request.Id);

            if(recipe == null || recipe.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Recipe),request.Id);
            }

            if (recipe.UserId != user.Id)
            {
                throw new UnauthorizedException();
            }
            // -- validacija
            validator.ValidateAndThrow(request);
            if (request.ExistingPictures.Any())
            {
                var recipeImagesIds = recipe.Images.Select(x => x.ImageId).ToList();
                var requestExistingImages = request.ExistingPictures.Select(x => x.Image).ToList();
                foreach (var id in requestExistingImages)
                {
                    if (!recipeImagesIds.Contains(id.Id))
                    {
                        throw new Application.Exceptions.ValidationException("ExistingPictures", $"This recipe does not have picture with id: {id.Id} linked to it");
                    }
                }
            }
            // -- 
             
            // RAD sa glupim slikama

            if(request.ExistingPictures.Count() == 0)
            {
                var recImgtoDelete = recipe.Images;
                var imagesToDelete = recImgtoDelete.Select(x=>x.Image).ToList();

                // da se obrise iz foldera, uzimamo putanje
                var paths = request.ExistingPictures.Select(x => x.Image.Path).ToList();

                context.RecipeImages.RemoveRange(recImgtoDelete);
                context.Images.RemoveRange(imagesToDelete);

                foreach(var path in paths)
                {
                    var fullPath = Path.Combine("wwwroot", "images", path);
                    File.Delete(fullPath);
                }

            }
            else if(request.ExistingPictures.Count() != recipe.Images.Count())
            {
                var epImage = request.ExistingPictures.Select(x => x.Image).ToList();
                var currentRecImages = recipe.Images.Select(x => x.Image).ToList();

                foreach(var image in currentRecImages)
                {
                    if(!epImage.Any(x => x.Id == image.Id))
                    {
                        var link = recipe.Images.Where(x => x.ImageId == image.Id).FirstOrDefault();
                        context.RecipeImages.Remove(link);
                        context.Images.Remove(image);

                        var path = Path.Combine("wwwroot", "images", image.Path);
                        File.Delete(path);
                    }
                }
            }

            // Brisanje prethodnih sastojaka
            var previousIngredients = recipe.Ingredients;
            context.IngredientRecipes.RemoveRange(previousIngredients);

            // Nove slike
            var newImages = new List<Image>();
            request.NewPictures.ForEach(x => {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(x.FileName);

                var newFileName = guid + "_" + Path.GetFileNameWithoutExtension(x.FileName) + extension;

                var path = Path.Combine("wwwroot", "images", newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    x.CopyTo(fileStream);
                }
                newImages.Add(new Image { Path = newFileName, Alt = Path.GetFileNameWithoutExtension(x.FileName) });
            });

            recipe.Title = request.Title;
            recipe.Description = request.Description;
            recipe.Guide = request.Guide;
            recipe.DietId = request.DietId;
            recipe.TimeToCook = request.TimeToCook;
            recipe.NumOfServings = request.NumOfServings;

            foreach(var newImage in newImages)
            {
                recipe.Images.Add(new RecipeImage
                {
                    Image = newImage
                });
            }

            foreach(var ing in request.Ingredients)
            {
                recipe.Ingredients.Add(new IngredientRecipe
                {
                    Quantity = $"{ing.Quantity}",
                    IngredientId = ing.IngredientId,
                    MeasureId = ing.MeasureId.Value
                });
            }

            context.SaveChanges();
        }
    }
}
