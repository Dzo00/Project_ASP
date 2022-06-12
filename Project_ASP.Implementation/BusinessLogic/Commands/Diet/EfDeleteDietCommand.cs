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
    public class EfDeleteDietCommand : IDeleteDietCommand
    {
        private readonly ProjectContext context;

        public int Id => 5;

        public string Name => "Delete Diet";

        public string Description => "Soft delete of diet in the system";

        public EfDeleteDietCommand(ProjectContext context)
        {
            this.context = context;
        }
        public void Execute(int request)
        {
            var diet = context.Diets.Find(request);
            if (diet == null || diet.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Diet), request);
            }

            if (diet.Recipes.Any()){
                throw new ConflictException(typeof(Diet), request, typeof(Recipe));
            }

            context.SoftDelete(diet);
            context.SaveChanges();

        }
    }
}
