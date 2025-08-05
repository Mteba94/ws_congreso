using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class RoleUsuario
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public int Estado { get; set; } // 1: Activo, 0: Inactivo
    }
}
