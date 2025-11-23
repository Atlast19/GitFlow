

using Dominio.Entidates;

namespace Aplicacion.Interface
{
    public interface IinscripcionRepositorio : IRepository<Inscripciones>
    {
        void EjecutarSPAgregarInscripcion(Inscripciones inscripcion);
    }
}
