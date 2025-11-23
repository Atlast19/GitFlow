
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidates
{
    public sealed class Inscripciones
    {
        [Key]
        public int IDInscripcion { get; set; }
        public int IDCurso { get; set; }
        public int IDUser { get; set; }
        public string EmailUser { get; set; }
    }
}
