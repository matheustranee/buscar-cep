using BuscarCep.Domain.DTO;
using System.Threading.Tasks;

namespace BuscarCep.Domain.Interfaces.Facades
{
    public interface IViaCepFacade
    {
        Task<IResult<CepDTO>> GetCep(int cep);        
    }
}
