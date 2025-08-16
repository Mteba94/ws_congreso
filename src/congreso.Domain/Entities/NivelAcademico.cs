using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class NivelAcademico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Estado { get; set; } // 1: Activo, 0: Inactivo
    }
}
