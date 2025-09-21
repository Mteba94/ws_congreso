using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Ponentes;

namespace congreso.Application.UseCase.Ponentes.Queries.GetById;

public class GetByIdPonenteQuery : IQuery<PonenteByIdResponseDto>
{
    public int PonenteId { get; set; }
}
