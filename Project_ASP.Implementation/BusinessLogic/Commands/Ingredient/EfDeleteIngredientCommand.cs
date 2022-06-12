using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.DataAccess.Extensions;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfDeleteIngredientCommand : IDeleteIngredientCommand
    {
        private readonly ProjectContext context;

        public int Id => 16;

        public string Name => "Delete Ingredient";

        public string Description => "Soft delete of Ingredient in the system";

        public EfDeleteIngredientCommand(ProjectContext context)
        {
            this.context = context;
        }
        public void Execute(int request)
        {
            var ingredient = context.Ingredients.Find(request);
            if (ingredient == null || ingredient.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Ingredient), request);
            }

            if (ingredient.Recipes.Any()){
                throw new ConflictException(typeof(Ingredient), request, typeof(Recipe));
            }

            context.SoftDelete(ingredient);
            context.SaveChanges();

        }
    }
}
