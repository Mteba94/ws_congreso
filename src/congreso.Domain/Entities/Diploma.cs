using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class Diploma : BaseEntity
    {
        public int InscripcionId {get; set;}
        public int ActividadId {get; set;}
        public int IdTipoDiploma {get; set;}
        public string NombreArchivo { get; set; } = null!;

        public Inscripcion Inscripcion { get; set; } = null!;
        public Actividad Actividad { get; set; } = null!;
    }
}
