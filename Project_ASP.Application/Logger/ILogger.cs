using Project_ASP.Application.BusinessLogic;
using Project_ASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.Logger
{
    public interface ILogger
    {
        void Log(Exception ex);
    }

    public interface IUseCaseLogger
    {
        void Log(UseCaseLog log);
    }
    public class UseCaseLog
    {
        public string ActionName { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public DateTime ExecutedOn { get; set; }
        public string Data { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
