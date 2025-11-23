

namespace Aplicacion.DTOs
{
    public sealed class UsuarioDTO
    {
        public int IDUser { get; set; }
        public string NameUser { get; set; }
        public DateOnly AgeUser { get; set; }
        public string MatriculaUser { get; set; }
        public string EmailUser { get; set; }
        public string PasswordUser { get; set; }
    }
}
