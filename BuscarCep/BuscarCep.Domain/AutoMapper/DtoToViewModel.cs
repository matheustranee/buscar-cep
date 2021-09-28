using AutoMapper;
using BuscarCep.Domain.DTO;
using BuscarCep.Domain.ViewModels;
using Flunt.Notifications;

namespace BuscarCep.Domain.AutoMapper
{
    public class DtoToViewModel : Profile
    {
        public DtoToViewModel()
        {
            CreateMap<CepDTO, CepViewModel>();

            CreateMap<Notification, NotificationViewModel>()
                .ForMember(viewModel => viewModel.Id, opt =>
                    opt.MapFrom(entity => entity.Message));
        }
    }
}
