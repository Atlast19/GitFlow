

namespace Aplicacion.Service.Base
{
    public interface IBaseService<T>
    {
        IEnumerable<T> GetAllRegistros();

        void PostRegistros(T model);

        T GetRegistrosByID(int ID);

        void DeleteById(int ID);

        void PutRegistros(T model);
    }
}
