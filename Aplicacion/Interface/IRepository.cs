

namespace Aplicacion.Interface
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAllRegistros();

        void PostRegistros(T model);

        T GetRegistrosByID(int ID);

        void DeleteById(int ID);

        void PutRegistros(T model);

    }
}
