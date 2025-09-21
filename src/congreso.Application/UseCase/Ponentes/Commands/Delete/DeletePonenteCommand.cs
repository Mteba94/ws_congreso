using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Ponentes.Commands.Delete
{
    public sealed class DeletePonenteCommand : ICommand<bool>
    {
        public int PonenteId { get; set; }
    }
}
