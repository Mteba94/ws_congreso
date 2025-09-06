using congreso.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace congreso.Application.UseCase.CodigosVerificacion.Commands.CreateCodigo;

public sealed class CreateCodigoCommand : ICommand<string>
{
    public string Purpose { get; set; } = null!;
    public string Email { get; set; } = null!;
}
