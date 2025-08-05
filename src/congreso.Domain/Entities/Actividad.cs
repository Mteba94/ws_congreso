using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class Actividad : BaseEntity
    {
        public int CongresoId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int TipoActividadId { get; set; }
        public DateTime FechaActividad { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int CuposDisponibles { get; set; }
        public int CuposTotales { get; set; }
        public string? Ubicacion { get; set; }
        public string? RequisitosPrevios { get; set; }

        public Congreso Congreso { get; set; } = null!;
        public TipoActividad TipoActividad { get; set; } = null!;
    }
}
