using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ASP.Application.Exceptions
{
    public class ConflictException : BaseApplicationException
    {
        public ConflictException(string entityName, object key, string referencedId)
            : base($"Can not delete Entity '{entityName}' with key '{key}' because it is referenced in {referencedId};")
        {
        }

        public ConflictException(Type objectType, object key, Type referencedId)
            : this(objectType.Name, key, referencedId.Name)
        {
        }
    }
}
