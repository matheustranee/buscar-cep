using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.IO;


namespace BuscarCep.Infra.CrossCutting.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            string enviroment = new WebHostBuilder().GetSetting("environment");

            enviroment ??= "Production";

            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"API Buscar Cep - {enviroment}"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Escreva \"bearer {token}\""
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                //options.IncludeXmlComments(Path.Combine("wwwroot", "api-docs.xml"));
            });
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                      .UseSwaggerUI(options =>
                      {
                          options.RoutePrefix = "swagger";
                          options.DocumentTitle = "API Buscar Cep - Swagger UI";
                          options.SwaggerEndpoint("../swagger/v1/swagger.json", "Documentação API v1");
                          options.DocExpansion(DocExpansion.List);
                          options.InjectStylesheet("/swagger-ui/custom.css");
                          options.InjectJavascript("/swagger-ui/custom.js", "text/javascript");
                      });
        }
    }
}
