using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.Exceptions
{
    public class UnauthorizedException : BaseApplicationException
    {
        public UnauthorizedException()
            :base("You do not have the permission to execute this action!")
        {

        }
    }
}
