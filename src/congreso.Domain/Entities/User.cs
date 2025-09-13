using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Pnombre { get; set; } = null!;
        public string? Snombre { get; set; }
        public string Papellido { get; set; } = null!;
        public string? Sapellido { get; set; }
        public string? image { get; set; }
        public int? TipoParticipanteId { get; set; }
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string? NumeroIdentificacion { get; set; }
        public int? SchoolId { get; set; }
        public int? NivelAcademicoId { get; set; }
        public int? Semestre { get; set; }
        public string Password { get; set; } = null!;
        public bool? EmailConfirmed { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public int? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public string SecurityStamp { get; set; } = null!;

        public TipoIdentificacion tipoIdentificacion { get; set; } = null!;
        public TipoParticipante tipoParticipante { get; set; } = null!;
        public NivelAcademico nivelAcademico { get; set; } = null!;
        public School School { get; set; } = null!;
    }
}
