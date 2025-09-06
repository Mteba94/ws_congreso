using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.Dtos.Congreso
{
    public class CongresoResponseDto
    {
        public int congresoId { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Estado { get; set; }
        public string? EstadoDescripcion { get; set; }
    }
}
