using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.Dtos.TipoParticipante;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings
{
    public class TipoParticipanteMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TipoParticipante, TipoParticipanteResponseDTO>()
            .Map(dest => dest.EstadoDescipcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

            config.NewConfig<TipoParticipante, SelectResponseDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Descripcion, src => src.Descripcion)
                .TwoWays();
        }
    }
}
