using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Estado { get; set; }
    }
}
