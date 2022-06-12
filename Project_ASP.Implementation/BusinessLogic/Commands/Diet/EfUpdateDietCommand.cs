using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using Project_ASP.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfUpdateDietCommand : IUpdateDietCommand
    {
        private readonly ProjectContext context;
        private readonly CreateDietValidator validator;

        public int Id => 4;

        public string Name => "Update Diet";

        public string Description => "Update data for diet";

        public EfUpdateDietCommand(ProjectContext context, CreateDietValidator validator)
        {
            this.context = context;
            this.validator = validator;
        }
        public void Execute(ILookupDto request)
        {
            var diet = context.Diets.Find(request.Id);
            if(diet == null || diet.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Diet),request.Id);
            }

            validator.ValidateAndThrow(request);

            diet.Name = request.Name;
            context.SaveChanges();
        }
    }
}
