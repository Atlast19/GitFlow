using Aplicacion.DTOs;
using Aplicacion.Interface;
using Aplicacion.Service.Base;
using AutoMapper;
using Dominio.Entidates;


namespace Aplicacion.Service
{
    public class UsuarioService : IBaseService<UsuarioDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Usuarios> _repository;
        private readonly ICorreos _correos;
        

        public UsuarioService(IMapper mapper, IRepository<Usuarios> repository, ICorreos correos)
        {
            _repository = repository;
            _mapper = mapper;
            _correos = correos;
        }

        public void DeleteById(int ID)
        {
            var model = new UsuarioDTO();
            _repository.DeleteById(ID);
        }

        public IEnumerable<UsuarioDTO> GetAllRegistros()
        {
            var list = _repository.GetAllRegistros();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(list);
        }

        public UsuarioDTO GetRegistrosByID(int ID)
        {
            var code = _repository.GetRegistrosByID(ID);
            return _mapper.Map<UsuarioDTO>(code);
        }

        public void PostRegistros(UsuarioDTO model)
        {
            DateTime fecha = DateTime.Now;
            var modelo = _mapper.Map<Usuarios>(model);
            _repository.PostRegistros(modelo);
            _correos.EnviarCorreo(model.EmailUser, "Bienvenido/a CursosGenerales", $"Hola {model.NameUser} Te has Registrado/a en nuestra plataforma web a las {fecha}");
        }

        public void PutRegistros(UsuarioDTO model)
        {
            DateTime fecha = DateTime.Now;
            var modelo = _mapper.Map<Usuarios>(model);
            _repository.PutRegistros(modelo);
            _correos.EnviarCorreo(model.EmailUser, "Datos actualizados en CursosGenerados", $"Hola {model.NameUser} Algunos de sus Datos Han sido Actualizados a las {fecha}");
        }
    }
}
