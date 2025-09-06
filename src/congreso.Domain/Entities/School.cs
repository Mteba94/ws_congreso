using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class School : CatalogoEntity
    {
        public string nombre { get; set; } = null!;
    }
}
