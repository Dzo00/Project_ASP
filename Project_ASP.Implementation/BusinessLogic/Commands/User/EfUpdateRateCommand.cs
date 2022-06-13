using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Exceptions;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using Project_ASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.BusinessLogic
{
    public class EfUpdateRateCommand : IUpdateRateCommand
    {
        private readonly ProjectContext context;
        private readonly IApplicationUser user;

        public int Id => 28;

        public string Name => "Change Rate";

        public string Description => "Update your rate";
        public EfUpdateRateCommand(ProjectContext context, IApplicationUser user)
        {
            this.context = context;
            this.user = user;
        }
        public void Execute(UpdateRateDto request)
        {
            var rate = context.Rates.Where(x => x.Id == request.Id).FirstOrDefault();

            if(rate == null)
            {
                throw new NotFoundException(typeof(Rate), request.Id);
            }

            if(rate.UserId != user.Id)
            {
                throw new UnauthorizedException();
            }

            if(request.RateValue == null)
            {
                throw new ValidationException("RateValue", "Value is required");
            }
            if(request.RateValue.Value < 1 && request.RateValue.Value > 5)
            {
                throw new ValidationException("RateValue", "Value can be 1 - 5");
            }

            rate.RateValue = request.RateValue.Value;
            context.SaveChanges();
        }
    }
}
