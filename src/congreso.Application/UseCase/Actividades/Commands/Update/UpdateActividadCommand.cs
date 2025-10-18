using congreso.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;

namespace congreso.Application.UseCase.Actividades.Commands.Update;

public sealed class UpdateActividadCommand : ICommand<bool>
{
    public int ActividadId { get; set; }
    public string Titulo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string DescripcionTotal { get; set; } = null!;
    public int TipoActividadId { get; set; }
    public DateTime FechaActividad { get; set; }
    public DateTime HoraInicio { get; set; }
    public DateTime HoraFin { get; set; }
    public int CuposTotales { get; set; }
    public string? Ubicacion { get; set; }
    public string? RequisitosPrevios { get; set; }
    public int NivelDificultadId { get; set; }
    public string? Imagen { get; set; }
    public int Orden { get; set; }
    public int permitirInscripcion { get; set; }
    public int Estado { get; set; } // For status updates

    // Related entities (Ponentes, Objetivos, Materiales) will require more complex handling in the handler
    public List<int>? Ponentes { get; set; }
    public List<string>? Objetivos { get; set; }
    public List<string>? Materiales { get; set; }
}