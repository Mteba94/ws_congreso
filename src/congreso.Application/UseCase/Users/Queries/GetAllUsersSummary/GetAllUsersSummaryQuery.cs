using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;

namespace congreso.Application.UseCase.Users.Queries.GetAllUsersSummary;

public sealed record GetAllUsersSummaryQuery : IQuery<IEnumerable<UserSummaryDto>>;
