using BuscarCep.CrossCutting.Extensions;
using BuscarCep.Domain.Settings;
using BuscarCep.Infra.Configurations.Configurations;
using BuscarCep.Infra.CrossCutting.Configurations;
using BuscarCep.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BuscarCep.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.InjectDependencies(Configuration);

            services.AddSwaggerConfiguration();

            services.AddAutoMapperSetup();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddMvc()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.UseCamelCasing(true);
                    });

            AddHttpClientsFromAppsettings(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuscarCep.API v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddHttpClientsFromAppsettings(IServiceCollection services)
        {
            List<ClientSettings> clients =
                Configuration.GetSection(nameof(ClientSettings)).Get<List<ClientSettings>>();

            foreach (ClientSettings clientSettings in clients)
            {
                services.AddHttpClient(clientSettings.Client.GetDescription(), opt =>
                {
                    opt.BaseAddress = new Uri(clientSettings.BaseUrl);
                });
            }

            services.AddHttpClient();
        }
    }
}
