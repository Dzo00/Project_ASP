using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.Exceptions
{
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string entityName, object key)
            : base($"Entity '{entityName}' with key '{key}' was not found;")
        {
        }

        public NotFoundException(Type objectType, object key)
            : this(objectType.Name, key)
        {
        }
    }
}
