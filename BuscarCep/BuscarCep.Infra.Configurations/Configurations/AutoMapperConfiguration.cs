using Microsoft.Extensions.DependencyInjection;
using BuscarCep.Domain.AutoMapper;
using System;

namespace BuscarCep.Infra.Configurations.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperProfileSetup));

            AutoMapperProfileSetup.RegisterMappingProfiles();
        }
    }
}
