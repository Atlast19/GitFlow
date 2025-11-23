
using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidates;

namespace Infraestructura.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            #region Mapeo de Usuarios
            CreateMap<UsuarioDTO, Usuarios>();
            CreateMap<Usuarios, UsuarioDTO>();
            #endregion

            #region Mapeo de Reservas
            CreateMap<Reservas, ReservasDTO>();
            CreateMap<ReservasDTO, Reservas>();
            #endregion

            #region Mapeo de Inscripciones
            CreateMap<Inscripciones, InscripcionesDTO>();
            CreateMap<InscripcionesDTO, Inscripciones>();
            #endregion
        }
    }
}
