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
    public class EfDeleteIngredientTypeCommand : IDeleteIngredientTypeCommand
    {
        private readonly ProjectContext context;

        public int Id => 11;

        public string Name => "Delete Ingredient Type";

        public string Description => "Soft delete of Ingredient Type in the system";

        public EfDeleteIngredientTypeCommand(ProjectContext context)
        {
            this.context = context;
        }
        public void Execute(int request)
        {
            var ingredientType = context.IngredientTypes.Find(request);
            if (ingredientType == null || ingredientType.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(IngredientType), request);
            }

            if (ingredientType.Ingredients.Any()){
                throw new ConflictException(typeof(IngredientType), request, typeof(Ingredient));
            }

            context.SoftDelete(ingredientType);
            context.SaveChanges();

        }
    }
}
