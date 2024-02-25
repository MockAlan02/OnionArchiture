using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException() : base("Se han producido uno o mas errores de validacion")
        {
            Errors = new List<string>();
        }
        public ValidationException(IEnumerable<ValidationFailure> failures): this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage); 
            }
        }
    }
}
