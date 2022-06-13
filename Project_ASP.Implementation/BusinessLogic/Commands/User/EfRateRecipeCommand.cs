using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using Project_ASP.Domain.Interfaces;
using Project_ASP.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfRateRecipeCommand : IRateRecipeCommand
    {
        private readonly ProjectContext context;
        private readonly AddRateValidator validator;
        private readonly IApplicationUser user;

        public int Id => 27;

        public string Name => "Rate Recipe";

        public string Description => "Add rate to recipe";

        public EfRateRecipeCommand(ProjectContext context, AddRateValidator validator, IApplicationUser user)
        {
            this.context = context;
            this.validator = validator;
            this.user = user;
        }
        public void Execute(CreateRateDto request)
        {
            validator.ValidateAndThrow(request);

            var exists = context.Rates.Where(x => x.RecipeId == request.RecipeId && x.UserId == user.Id).FirstOrDefault();
            if(exists != null)
            {
                throw new ConflictException("You have already rated this recipe");
            }

            var newRate = new Rate
            {
                RecipeId = request.RecipeId,
                UserId = user.Id,
                RateValue = request.RateValue
            };

            context.Rates.Add(newRate);
            context.SaveChanges();
        }
    }
}
