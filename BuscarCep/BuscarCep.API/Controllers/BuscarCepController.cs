using AutoMapper;
using BuscarCep.Domain.DTO;
using BuscarCep.Domain.Interfaces;
using BuscarCep.Domain.Interfaces.Facades;
using BuscarCep.Domain.ViewModels;
using BuscarCep.Services.Api.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuscarCep.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BuscarCepController : BaseController
    {
        public BuscarCepController(IMapper mapper,
                                   IViaCepFacade viacepFacade) : base(viacepFacade, mapper)
        {

        }

        /// <summary>
        /// Retorna informações CEP
        /// </summary>
        [HttpGet("buscar-cep")]
        [ProducesResponseType(typeof(ResponseViewModel<CepViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCep([FromServices] IViaCepFacade viacepFacade, 
                                                [FromQuery] int cep)
        {
            IResult<CepDTO> cepDTO =
                await viacepFacade.GetCep(cep);

            return await GetResult<CepDTO, CepViewModel>(cepDTO);
        }
    }
}
