using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Congreso;

namespace congreso.Application.UseCase.Congresos.Queries.GetById;

public sealed class GetCongresoByIdQuery : IQuery<CongresoByIdResponseDto>
{
    public int CongresoId { get; set; }
}
