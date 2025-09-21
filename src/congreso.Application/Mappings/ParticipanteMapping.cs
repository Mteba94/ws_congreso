using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Participantes;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

internal class ParticipanteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, ParticipantesResponseDto>()
            .Map(dest => dest.ParticipanteId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .Map(dest => dest.NivelAcademico, src => src.NivelAcademicoId)
            .TwoWays();

        config.NewConfig<User, ParticipanteByIdResponseDto>()
            .Map(dest => dest.ParticipanteId, src => src.Id)
            .Map(dest => dest.NivelAcademico, src => src.NivelAcademicoId)
            .TwoWays();

        config.NewConfig<User, SelectResponseDto>()
          .Map(dest => dest.Id, src => src.Id)
          .Map(dest => dest.Nombre, src => src.Pnombre + " " + src.Snombre + " " + src.Papellido + " " + src.Sapellido)
          .TwoWays();

        config.NewConfig<CreateUserCommand, User>();
    }
}
