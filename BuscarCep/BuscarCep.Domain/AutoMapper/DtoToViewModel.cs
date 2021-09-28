using AutoMapper;
using BuscarCep.Domain.DTO;
using BuscarCep.Domain.ViewModels;

namespace BuscarCep.Domain.AutoMapper
{
    public class DtoToViewModel : Profile
    {
        public DtoToViewModel()
        {
            CreateMap<CepDTO, CepViewModel>();
        }
    }
}
