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
    public class EfAddCommentCommand : IAddCommentCommand
    {
        private readonly ProjectContext context;
        private readonly AddCommentValidator validator;
        private readonly IApplicationUser user;

        public int Id => 29;

        public string Name => "Add comment";

        public string Description => "Add comment to recipe";
        public EfAddCommentCommand(ProjectContext context, AddCommentValidator validator, IApplicationUser user)
        {
            this.context = context;
            this.validator = validator;
            this.user = user;
        }
        public void Execute(CreateCommentDto request)
        {
            validator.ValidateAndThrow(request);

            var newComment = new Comment
            {
                RecipeId = request.RecipeId,
                UserId = user.Id,
                CommentText = request.CommentText
            };

            context.Comments.Add(newComment);
            context.SaveChanges();
        }
    }
}
