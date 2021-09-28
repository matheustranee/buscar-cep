using BuscarCep.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace BuscarCep.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string _environment = new WebHostBuilder().GetSetting("environment");

            IConfigurationRoot _config =
                new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{_environment}.json", optional: true, reloadOnChange: true)
                    .Build();

            try
            {
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
