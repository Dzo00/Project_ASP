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
    public class EfUpdateUserCommand : IUpdateUserCommand
    {
        private readonly ProjectContext context;
        private readonly UpdateAccountValidator validator;
        private readonly IApplicationUser _user;

        public int Id => 25;

        public string Name => "Update Account";

        public string Description => "Update account detail";

        public EfUpdateUserCommand(ProjectContext context, UpdateAccountValidator validator, IApplicationUser user)
        {
            this.context = context;
            this.validator = validator;
            this._user = user;
        }
        public void Execute(UpdateUserDto request)
        {
            var user = context.Users.Find(request.Id);
            if (user == null || user.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(User), request.Id);
            }
            if (user.Id != _user.Id)
            {
                throw new UnauthorizedException();
            }

            validator.ValidateAndThrow(request);
            if(request.DisplayName != null)
            {
                user.DisplayName = request.DisplayName;
            }
            if (request.Password != null)
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.Password = hash;
            }
            context.SaveChanges();
        }
    }
}
