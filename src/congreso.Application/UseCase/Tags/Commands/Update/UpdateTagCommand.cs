using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Tags.Commands.Update;

public sealed class UpdateTagCommand : ICommand<bool>
{
    public int TagId { get; set; }
    public string NombreTag { get; set; } = null!;
}
