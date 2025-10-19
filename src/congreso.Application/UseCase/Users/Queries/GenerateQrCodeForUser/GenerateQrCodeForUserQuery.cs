using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;
using congreso.Domain.Entities;

namespace congreso.Application.UseCase.Users.Queries.GenerateQrCodeForUser;

public sealed record GenerateQrCodeForUserQuery(int UserId, int actividadId) : IQuery<GenerateUserQrCodeResponseDto>;
