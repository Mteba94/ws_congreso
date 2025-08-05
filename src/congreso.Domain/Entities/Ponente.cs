using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class Ponente : BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Titulo { get; set; }
        public string? Empresa { get; set; }
        public string? Biografia { get; set; }
        public string? Foto { get; set; }
    }
}
