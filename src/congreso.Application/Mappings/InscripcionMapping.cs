using congreso.Application.UseCase.Inscripciones.Commands.Create;
using congreso.Domain.Entities;
using Mapster;

namespace congreso.Application.Mappings;

public class InscripcionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateInscripcionCommand, Inscripcion>()
            .Map(dest => dest.ActividadId, src => src.IdActividad)
            .Map(dest => dest.UserId, src => src.IdUsuario)
            .TwoWays();
    }
}
