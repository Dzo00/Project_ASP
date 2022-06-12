using FluentValidation;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.BusinessLogic.DTO.Application;
using Project_ASP.Application.Emails;
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
    public class EfCreateNewAdminCommand : IAddNewAdminCommand
    {
        public int Id => 22;

        public string Name => "Add new Admin";

        public string Description => "Creation of a new admin";

        private readonly ProjectContext _context;
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _emailSender;

        public EfCreateNewAdminCommand(ProjectContext context, RegisterUserValidator validator, IEmailSender emailSender)
        {
            _context = context;
            _validator = validator;
            _emailSender = emailSender;
        }

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                Password = hash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                RoleId = 2,
                DisplayName = $"{request.FirstName} {request.LastName}"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // zakomentarisan zbog pucanja

            //_emailSender.Send(new MessageDto
            //{
            //    To = request.Email,
            //    Title = "Successfull registration!",
            //    Body = $"Dear {request.FirstName} {request.LastName} \n Please click the link to activate your account. \n Activation link: .... Your generated Password is {request.Password}, please change it after first login!"
            //});
        }
    }
}
