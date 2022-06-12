using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public DateTime ExecutedOn { get; set; }
        public string Data { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
