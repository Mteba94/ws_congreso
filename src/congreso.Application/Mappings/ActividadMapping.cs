using congreso.Application.Dtos.Actividades;
using congreso.Application.UseCase.Actividades.Commands.Create;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class ActividadMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Actividad, ActividadResponseDto>()
            .Map(dest => dest.ActividadId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<Actividad, ActividadByIdResponseDto>()
            .Map(dest => dest.ActividadId, src => src.Id)
            .TwoWays();

        config.NewConfig<CreateActividadCommand, Actividad>()
            .Map(dest => dest.FechaActividad, src => src.Fecha)
            .Map(dest => dest.CuposTotales, src => src.CuposTotal)
            .Map(dest => dest.RequisitosPrevios, src => src.Requisitos)
            .TwoWays();

        config.NewConfig<TimeOnly, DateTime>()
              .MapWith(src => new DateTime(1, 1, 1, src.Hour, src.Minute, src.Second));

        config.NewConfig<DateTime, TimeOnly>()
              .MapWith(src => TimeOnly.FromDateTime(src));
    }
}
