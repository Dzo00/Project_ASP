using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO;
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
    public class EfCreateDietCommand : ICreateDietCommand
    {
        private CreateDietValidator _validator;
        ProjectContext _context;
        public EfCreateDietCommand(
            ProjectContext context,
            CreateDietValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Create Diet";

        public string Description => "Add new diet";

        public void Execute(ILookupDto request)
        {
            _validator.ValidateAndThrow(request);

            var diet = new Diet
            {
                Name = request.Name
            };

            _context.Diets.Add(diet);
            _context.SaveChanges();
            
        }
    }
}
