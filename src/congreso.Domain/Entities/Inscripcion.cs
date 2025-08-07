using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class Inscripcion : BaseEntity
    {
        public int UserId { get; set; }
        public int ActividadId { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public User User { get; set; } = null!;
        public Actividad Actividad { get; set; } = null!;
        public ICollection<Asistencia>? Asistencias { get; set; }
        public ICollection<Diploma>? Diplomas { get; set; }
    }
}
