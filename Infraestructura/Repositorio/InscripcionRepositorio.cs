

using Aplicacion.Interface;
using Dominio.Entidates;
using Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorio
{
    public class InscripcionRepositorio : Repositorio<Inscripciones>, IinscripcionRepositorio
    {
        private readonly Contexto _contexto;

        public InscripcionRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public void EjecutarSPAgregarInscripcion(Inscripciones inscripcion)
        {
            _contexto.Database.ExecuteSqlRaw(
             "EXEC sp_AgregarInscripcion @p0, @p1, @p2",
             inscripcion.IDCurso,
             inscripcion.IDUser,
             inscripcion.EmailUser
         );
        }
    }
}
