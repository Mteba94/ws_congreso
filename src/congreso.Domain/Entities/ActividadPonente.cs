using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class ActividadPonente
    {
        public int id { get; set; }
        public int ActividadId { get; set; }
        public int PonenteId { get; set; }
        public int Estado { get; set; }

        public Actividad Actividad { get; set; } = null!;
        public Ponente Ponente { get; set; } = null!;
    }
}
