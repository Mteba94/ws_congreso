using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.CodigosVerificacion.Queries.ValidacionCodigo;

public class ValidacionCodigoQuery : IQuery<bool>
{
    public string Codigo { get; set; } = null!;
    public string Purpose { get; set; } = null!;
    public string Email { get; set; } = null!;
}
