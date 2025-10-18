using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;

namespace congreso.Application.UseCase.Users.Queries.GetUserCertificateCount;

public sealed record GetUserCertificateCountQuery(int UserId) : IQuery<UserCertificateCountDto>;
