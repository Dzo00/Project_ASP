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
    public class EfDeleteAccountCommand : IDeleteAccountCommand
    {
        private readonly ProjectContext context;
        private readonly IApplicationUser _user;

        public int Id => 26;

        public string Name => "Delete account";

        public string Description => "Delete account";

        public EfDeleteAccountCommand(ProjectContext context, IApplicationUser user)
        {
            this.context = context;
            this._user = user;
            this.context = context;
        }
        public void Execute(int request)
        {
            var user = context.Users
                              .Include(x => x.Comments)
                              .Include(x => x.Recipes)
                              .Include(x => x.Rates)
                              .Where(x=>x.Id == request).FirstOrDefault();
            
            if (user == null || user.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(User), request);
            }

            if (user.Id != _user.Id)
            {
                throw new UnauthorizedException();
            }

            if (user.Rates.Any())
            {
                var rateIds = user.Rates.Select(x => x.Id).ToList();
                context.SoftDelete<Rate>(rateIds);
            }
            if (user.Comments.Any())
            {
                var commentIds = user.Comments.Select(x => x.Id).ToList();
                context.SoftDelete<Comment>(commentIds);
            }

            if (user.Rates.Any())
            {
                var recipeIds = user.Recipes.Select(x => x.Id).ToList();
                context.SoftDelete<IngredientRecipe>(recipeIds);
            }

            context.SoftDelete(user);
            context.SaveChanges();

        }
    }
}
