using AutoMapper;
using BuscarCep.Domain.Interfaces;
using BuscarCep.Domain.Interfaces.Facades;
using BuscarCep.Domain.ViewModels;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscarCep.Services.Api.Configurations
{
    public class BaseController : ControllerBase
    {
        private readonly IViaCepFacade _viacepFacade;
        private readonly IMapper _mapper;

        public BaseController(IViaCepFacade viacepFacade,
                              IMapper mapper)
        {
            _viacepFacade = viacepFacade;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetResult<T>(IResult<T> result)
        {
            IEnumerable<Notification> notifications = result.GetNotifications();

            var response = new ResponseViewModel<T>
            {
                IsSuccess = !notifications.Any(),
                Data = result.GetData(),
                Message = result.GetMessage()
            };

            return HandleResponseAsync(response);
        }

        public async Task<IActionResult> GetResult<T, TMapTo>(IResult<T> result)
        {
            IEnumerable<Notification> notifications = result.GetNotifications();

            var response = new ResponseViewModel<TMapTo>
            {
                IsSuccess = !notifications.Any(),
                Data = _mapper.Map<TMapTo>(result.GetData()),
                Message = result.GetMessage()
            };
            
            return HandleResponseAsync(response);
        }

        private IActionResult HandleResponseAsync<T>(ResponseViewModel<T> response)
        {
            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }       
    }
}
