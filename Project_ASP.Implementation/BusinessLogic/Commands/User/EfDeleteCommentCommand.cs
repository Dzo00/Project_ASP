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
    public class EfDeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly ProjectContext context;
        private readonly IApplicationUser user;

        public int Id => 30;

        public string Name => "Delete Comment";

        public string Description => "Delete personal comment";

        public EfDeleteCommentCommand(ProjectContext context, IApplicationUser user)
        {
            this.context = context;
            this.user = user;
        }

        public void Execute(int request)
        {
            var comment = context.Comments.Find(request);

            if (comment == null || comment.EntityStatus == Domain.Enums.eEntityStatus.Deleted)
            {
                throw new NotFoundException(typeof(Comment), request);
            }

            if (comment.UserId != user.Id)
            {
                throw new UnauthorizedException();
            }

            context.SoftDelete(comment);
            context.SaveChanges();
        }
    }
}
