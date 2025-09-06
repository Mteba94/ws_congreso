using congreso.Application.UseCase.CodigosVerificacion.Commands.CreateCodigo;
using congreso.Domain.Entities;
using Mapster;

namespace congreso.Application.Mappings;

public class CodigoMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCodigoCommand, CodigoVerificacion>();
    }
}
