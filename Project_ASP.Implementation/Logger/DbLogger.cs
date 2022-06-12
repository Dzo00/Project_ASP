using Project_ASP.Application.Logger;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Logger
{
    public class DbLogger : IUseCaseLogger
    {
        private ProjectContext _context;
        public DbLogger(ProjectContext context) => _context = context;

        public void Log(UseCaseLog log)
        {
            var newLog = new Log
            {
                Username = log.Username,
                ExecutedOn = log.ExecutedOn,
                UserId = log.UserId,
                Data = log.Data,
                IsAuthorized = log.IsAuthorized,
                ActionName = log.ActionName,
            };
            _context.Logs.Add(newLog);
            _context.SaveChanges();
        }
    }
}
