using congreso.Application.Abstractions.Messaging;
using congreso.Application.UseCase.Ponentes.Commands.Create;

namespace congreso.Application.UseCase.Ponentes.Commands.Update;

public class UpdatePonenteCommand : ICommand<bool>
{
    public int PonenteId { get; set; }
    public string NombrePonente { get; set; } = null!;
    public string ApellidoPonente { get; set; } = null!;
    public string TituloPonente { get; set; } = null!;
    public string? EmpresaPonente { get; set; }
    public string? BioPonente { get; set; }
    public string? ImagenPonente { get; set; }

    public ICollection<PonenteTagUpdateDto> PonenteTags { get; set; } = null!;
}

public class PonenteTagUpdateDto
{
    public int TagId { get; set; }
    public bool Seleccionado { get; set; }
}
