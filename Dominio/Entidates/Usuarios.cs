

using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Dominio.Entidates
{
    public sealed class Usuarios
    {
        [Key]
        public int IDUser { get; set; }
        public string NameUser { get; set; }
        public string PasswordUser { get; set; }
        public string MatriculaUser { get; set; }
        public DateOnly AgeUser { get; set; }
        public string EmailUser { get; set; }
    }
}
