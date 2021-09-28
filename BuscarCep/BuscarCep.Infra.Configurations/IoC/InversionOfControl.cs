using BuscarCep.CrossCutting.ExternalApis;
using BuscarCep.CrossCutting.WebRequest;
using BuscarCep.Domain.Interfaces.Facades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuscarCep.Infra.CrossCutting.IoC
{
    public static class InversionOfControl
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {          
            #region Facades

            services.AddScoped<IViaCepFacade, ViaCepFacade>();
            services.AddScoped<IRequestFacade, RequestFacade>();

            #endregion
        }
    }
}
