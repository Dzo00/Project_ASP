using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Project_ASP.Application.Exceptions
{
    public class ValidationException : BaseApplicationException
    {
        public Dictionary<string, string[]> Failures { get; set; }
        public ValidationException() 
            : base("One or more validation failures have occured")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(string error)
            : base(error)
        {
        }

        public ValidationException(string propertyName, string error)
            : this()
        {
            Failures.Add(propertyName, new string[1] { error });
        }
    }
}
