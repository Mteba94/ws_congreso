using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class Asistencia : BaseEntity
    {
        public int InscripcionId { get; set; }
        public int ActividadId { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Inscripcion Inscripcion { get; set; } = null!;
        public Actividad Actividad { get; set; } = null!;
    }
}
