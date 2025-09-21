using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Tags.Commands.Delete;

public sealed class DeleteTagCommand : ICommand<bool>
{
    public int TagId { get; set; }
}
