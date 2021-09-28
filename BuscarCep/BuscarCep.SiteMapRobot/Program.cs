using BuscarCep.Domain.Interfaces.Facades;
using BuscarCep.Infra.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;

namespace BuscarCep.SitemapRobot
{
    class Program
    {
        private static IConfigurationRoot _configuration;

        static void Main(string[] args)
        {
            try
            {
                ServiceProvider serviceProvider = Startup();

                IRequestFacade _request = serviceProvider.GetService<IRequestFacade>();                
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Erro");
            }
        }

        static ServiceProvider Startup()
        {
            #region Appsettings

            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationBuilder builder =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();

            #endregion

            #region IoC

            IServiceCollection _collection = new ServiceCollection();

            InversionOfControl.InjectDependencies(_collection, _configuration);

            #endregion            

            return _collection.AddOptions()
                              .BuildServiceProvider();
        }

    }
}
