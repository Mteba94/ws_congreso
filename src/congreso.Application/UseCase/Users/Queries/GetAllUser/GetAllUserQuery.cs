using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Participantes;
using congreso.Application.Dtos.User;

namespace congreso.Application.UseCase.Users.Queries.GetAllUser
{
    public sealed class GetAllUserQuery : BaseFilters, IQuery<IEnumerable<ParticipantesResponseDto>>
    {

    }
}
