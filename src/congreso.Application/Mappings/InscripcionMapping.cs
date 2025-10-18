using congreso.Application.Dtos.Inscripciones;
using congreso.Application.UseCase.Inscripciones.Commands.Create;
using congreso.Domain.Entities;
using Mapster;

namespace congreso.Application.Mappings;

public class InscripcionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Inscripcion, InscripcionesByUserDTO>()
            .Map(dest => dest.InscripcionId, src => src.Id)
            .Map(dest => dest.ActividadId, src => src.ActividadId)
            .Map(dest => dest.Puntaje, src => src.Puntaje)
            .Map(dest => dest.EsGanador, src => src.EsGanador)
            .TwoWays();

        config.NewConfig<Inscripcion, InscripcionesResponseDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.ActividadId, src => src.ActividadId)
            .Map(dest => dest.FechaInscripcion, src => src.FechaInscripcion)
            .Map(dest => dest.Puntaje, src => src.Puntaje)
            .Map(dest => dest.EsGanador, src => src.EsGanador)
            .TwoWays();

        config.NewConfig<CreateInscripcionCommand, Inscripcion>()
            .Map(dest => dest.ActividadId, src => src.IdActividad)
            .Map(dest => dest.UserId, src => src.IdUsuario)
            .TwoWays();
    }
}
