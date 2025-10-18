using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;

namespace congreso.Application.UseCase.Users.Queries.GetUserInscriptionsCount;

public sealed record GetUserInscriptionsCountQuery(int UserId) : IQuery<UserInscriptionsCountDto>;
