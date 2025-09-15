using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Menus;

namespace congreso.Application.UseCase.Menus.Queries.GetById;

public class GetMenuByUserIdQuery : IQuery<IEnumerable<MenuResponseDto>>
{
    public int UserId { get; set; }
}
