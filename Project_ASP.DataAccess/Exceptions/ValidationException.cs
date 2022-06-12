using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Project_ASP.DataAccess.Exceptions
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

        public ValidationException(List<ValidationFailure> failures)
            :this()
        {
            var propertyNames = failures.Select(x => x.PropertyName).Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures.Where(x => x.PropertyName == propertyName).Select(x => x.ErrorMessage).ToArray();
                Failures.Add(propertyName, propertyFailures);
            }

        }

        public ValidationException(string propertyName, string error)
            : this()
        {
            Failures.Add(propertyName, new string[1] { error });
        }
    }
}
