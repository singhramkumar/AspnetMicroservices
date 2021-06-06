using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exception
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("One or more validation error has occured")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures.GroupBy(c => c.PropertyName, c => c.ErrorMessage)
                .ToDictionary(f => f.Key, f => f.ToArray());
        }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
