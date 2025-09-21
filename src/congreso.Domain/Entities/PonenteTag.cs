using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class PonenteTag : CatalogoEntity
    {
        public int TagId { get; set; }
        public int PonenteId { get; set; }

        public Tag Tag { get; set; } = null!;
        public Ponente Ponente { get; set; } = null!;
    }
}
