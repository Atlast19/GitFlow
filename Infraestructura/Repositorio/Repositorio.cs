using Aplicacion.Interface;
using Infraestructura.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorio
{
    public class Repositorio<T> : IRepository<T> where T : class
    {
        private readonly Contexto _contexto;
        DbSet<T> _dbSet;

        public Repositorio(Contexto contexto)
        {
            _contexto = contexto;
            _dbSet = contexto.Set<T>();
        }

        public void DeleteById(int ID)
        {
                var Code = GetRegistrosByID(ID);
                _dbSet.Remove(Code);
                _contexto.SaveChanges();
        }

        public IEnumerable<T> GetAllRegistros()
        {
                return _dbSet.ToList();
        }

        public T GetRegistrosByID(int ID)
        {
            return _dbSet.Find(ID);
        }

        public void PostRegistros(T model)
        {
            _dbSet.Add(model);
            _contexto.SaveChanges();      

        }

        public void PutRegistros(T model)
        {
            _dbSet.Entry(model).State = EntityState.Modified;
            _contexto.SaveChanges();
        }
    }
}
