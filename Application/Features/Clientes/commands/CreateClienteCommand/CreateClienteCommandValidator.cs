using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.commands.CreateClienteCommand
{

    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand> 
    {
        public CreateClienteCommandValidator()
        {
            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("No puede ser nulo").Length(4,10).WithMessage("Longitud de letras invalidad");
            RuleFor(c => c.FechaNacimiento).NotEmpty().WithMessage("No puede ser nulo");
            RuleFor(c => c.Telefono).NotEmpty().WithMessage("No puede ser nulo").Matches(@"\A\([1-9]{3}\) [0-9]{3}-[0-9]{4}\z").MaximumLength(10);
            RuleFor(c => c.Email).NotEmpty().WithMessage("No puede ser nulo").EmailAddress().WithMessage("Ingrese un email valido");
            RuleFor(c => c.Telefono).NotEmpty().WithMessage("No puede ser nulo").Length(10, 40).WithMessage("Ingrese una direccion valida");
        }
    }
}
