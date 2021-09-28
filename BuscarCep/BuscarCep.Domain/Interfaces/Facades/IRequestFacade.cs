using BuscarCep.Domain.WebRequest;
using System.Threading.Tasks;

namespace BuscarCep.Domain.Interfaces.Facades
{
    public interface IRequestFacade
    {
        Task<Response> CreateRequestAsync(Request request);        
    }
}
