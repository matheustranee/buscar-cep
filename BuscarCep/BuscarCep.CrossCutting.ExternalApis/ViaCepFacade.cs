using BuscarCep.CrossCutting.Extensions;
using BuscarCep.Domain.Bases;
using BuscarCep.Domain.Builders;
using BuscarCep.Domain.DTO;
using BuscarCep.Domain.Enum;
using BuscarCep.Domain.Interfaces;
using BuscarCep.Domain.Interfaces.Facades;
using BuscarCep.Domain.WebRequest;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuscarCep.CrossCutting.ExternalApis
{
    public class ViaCepFacade : BaseNotifiable, IViaCepFacade
    {
        private static readonly EClient CLIENT = EClient.ViaCep;

        private readonly IRequestFacade _requestFacade;

        public ViaCepFacade(IRequestFacade requestFacade)
        {
            _requestFacade = requestFacade;
        }

        public async Task<IResult<CepDTO>> GetCep(int cep)
        {
            if (cep.ToString().Length != 8)
                return Error<CepDTO>(Notifications, $"Invalid CEP");

            Request request =
                RequestBuilder.Create(CLIENT, HttpMethod.Get, cep.ToString() + "/json")
                              .Build();

            Response response =
                await _requestFacade.CreateRequestAsync(request);

            if (response.IsSuccessStatusCode)
            {
                CepDTO cepDto =
                    response.DeserializeSuccessData<CepDTO>();

                return Success(cepDto);
            }
            else
            {
                return Error<CepDTO>(Notifications, $"Error on client {CLIENT.GetDescription()}");
            }
        }
    }
}
