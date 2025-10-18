using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;

namespace congreso.Application.UseCase.Users.Queries.GetUserAttendancePercentage;

public sealed record GetUserAttendancePercentageQuery(int UserId) : IQuery<UserAttendancePercentageDto>;
