using congreso.Application.Abstractions.Messaging;
using congreso.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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
    public IFormFile? Imagen {  get; set; }

    public string? ActividadPonente { get; set; } = null!;
    public string? ObjetivosActividad { get; set; }
    public string? MaterialesActividad { get; set; }

    public ActividadPonenteRequest? GetPonente() =>
        string.IsNullOrWhiteSpace(ActividadPonente)
            ? null
            : JsonSerializer.Deserialize<ActividadPonenteRequest>(ActividadPonente);

    public ICollection<ObjetivosActividadRequest>? GetObjetivos() =>
        string.IsNullOrWhiteSpace(ObjetivosActividad)
            ? null
            : JsonSerializer.Deserialize<ICollection<ObjetivosActividadRequest>>(ObjetivosActividad);

    public ICollection<MaterialesActividadRequest>? GetMateriales() =>
        string.IsNullOrWhiteSpace(MaterialesActividad)
            ? null
            : JsonSerializer.Deserialize<ICollection<MaterialesActividadRequest>>(MaterialesActividad);
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
