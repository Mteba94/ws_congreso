using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Ponentes.Commands.Create;

public sealed class CreatePonenteCommand : ICommand<bool>
{
    public string NombrePonente { get; set; } = null!;
    public string ApellidoPonente { get; set; } = null!;
    public string TituloPonente { get; set; } = null!;
    public string? EmpresaPonente { get; set; }
    public string? BioPonente { get; set; }
    public string? ImagenPonente { get; set; }

    public ICollection<PonenteTagRequestDto> PonenteTags { get; set; } = null!;
}

public class PonenteTagRequestDto
{
    public int TagId { get; set; }
    public bool Seleccionado { get; set; }
}
