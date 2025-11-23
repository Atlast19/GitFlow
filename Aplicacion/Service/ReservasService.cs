using Aplicacion.DTOs;
using Aplicacion.Interface;
using Aplicacion.Service.Base;
using AutoMapper;
using Dominio.Entidates;


namespace Aplicacion.Service
{
    public class ReservasService : IBaseService<ReservasDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Reservas> _repository;  

        public ReservasService(IRepository<Reservas> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void DeleteById(int ID)
        {
            _repository.DeleteById(ID);
        }

        public IEnumerable<ReservasDTO> GetAllRegistros()
        {
            var list = _repository.GetAllRegistros().ToList();
            return _mapper.Map<IEnumerable<ReservasDTO>>(list);
        }

        public ReservasDTO GetRegistrosByID(int ID)
        {
           var code = _repository.GetRegistrosByID(ID);
            return _mapper.Map<ReservasDTO>(code);
        }

        public void PostRegistros(ReservasDTO model)
        {
            var models = _mapper.Map<Reservas>(model);
            _repository.PostRegistros(models);
            
        }

        public void PutRegistros(ReservasDTO model)
        {
            var models = _mapper.Map<Reservas>(model);
            _repository.PutRegistros(models);
        }

    }
}
