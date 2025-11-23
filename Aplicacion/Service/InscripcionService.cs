
using Aplicacion.DTOs;
using Aplicacion.Interface;
using Aplicacion.Service.Base;
using AutoMapper;
using Dominio.Entidates;
using System.Diagnostics.Metrics;

namespace Aplicacion.Service
{
    public class InscripcionService : IBaseService<InscripcionesDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Inscripciones> _repository;
        private readonly IinscripcionRepositorio _inscripcionRepositorio;
        private readonly ICorreos _correos;
        public InscripcionService(IMapper mapper, IRepository<Inscripciones> repository, IinscripcionRepositorio iinscripcion, ICorreos correos)
        {
            _mapper = mapper;
            _repository = repository;
            _inscripcionRepositorio = iinscripcion;
            _correos = correos;
        }
        public void DeleteById(int ID)
        {
            _repository.DeleteById(ID);
        }

        public IEnumerable<InscripcionesDTO> GetAllRegistros()
        {
            var list =_repository.GetAllRegistros().ToList();
            return _mapper.Map<IEnumerable<InscripcionesDTO>>(list);
        }

        public InscripcionesDTO GetRegistrosByID(int ID)
        {
            var code = _repository.GetRegistrosByID(ID);
            return _mapper.Map<InscripcionesDTO>(code);
        }

        public void PostRegistros(InscripcionesDTO model)
        {
            DateTime fecha = DateTime.Now;
            var models = _mapper.Map<Inscripciones>(model);
            _inscripcionRepositorio.EjecutarSPAgregarInscripcion(models);
            _correos.EnviarCorreo(models.EmailUser, "Te agregaste a un curso en CursosGenerales", $"Estimado/a usuario usted se ha registrado en un curso a las {fecha} y el codigo de tu curso es el: {models.IDCurso}");
        }

        public void PutRegistros(InscripcionesDTO model)
        {
            DateTime fecha = DateTime.Now;
            var models = _mapper.Map<Inscripciones>(model);
            _repository.PutRegistros(models);
            _correos.EnviarCorreo(model.EmailUser, "Datos actualizados en CursosGenerados", $" Algunos de sus Datos Han sido Actualizados a las {fecha}");
        }
    }
}
