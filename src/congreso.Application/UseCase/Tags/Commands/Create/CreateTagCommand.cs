using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Tags.Commands.Create;

public sealed class CreateTagCommand : ICommand<bool>
{
    public string nombreTag { get; set; } = null!;
}
