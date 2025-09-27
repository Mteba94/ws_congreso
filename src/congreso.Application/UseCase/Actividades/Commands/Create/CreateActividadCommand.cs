using congreso.Application.Abstractions.Messaging;

namespace congreso.Application.UseCase.Actividades.Commands.Create;

public sealed class CreateActividadCommand : ICommand<bool>
{
    public int CongresoId { get; set; }
    public string Titulo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string DescripcionTotal { get; set; } = null!;
    public int TipoActividadId { get; set; }
    public DateTime Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public int CuposTotal { get; set; }
    public string? Ubicacion { get; set; }
    public string? Requisitos { get; set; }
    public int NivelDificultadId {  get; set; }
    public string? Imagen {  get; set; }

    public ActividadPonenteRequest ActividadPonente { get; set; } = null!;
    public ICollection<ObjetivosActividadRequest>? ObjetivosActividad { get; set; }
    public ICollection<MaterialesActividadRequest>? materialesActividad { get; set; }
}

public class ActividadPonenteRequest
{
    public int PonenteId { get; set; }
}

public class ObjetivosActividadRequest
{
    public string? ObjetoDesc {  get; set; }
}

public class MaterialesActividadRequest
{
    public string? MaterialDesc { get; set; }
}
