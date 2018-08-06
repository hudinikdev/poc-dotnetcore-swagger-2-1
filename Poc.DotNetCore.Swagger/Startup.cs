using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger; // Adicionar.
using System; // Adicionar.
using System.IO; // Adicionar.
using System.Reflection; // Adicionar.

namespace Poc.DotNetCore.Swagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Registra o gerador do Swagger.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "POC .NET Core + Swagger",
                    Description = "API desenvolvida para exemplificar o uso do Swagger",                    
                    Contact = new Contact
                    {
                        Name = "João Pedro Hudinik",
                        Email = "hudinik@outlook.com",
                        Url = "https://medium.com/@joaopedrohudinik"
                    }                    
                });

                // Defina o caminho dos comentários para o Swagger.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Habilita o middleware responsável por gerar o JSON do Swagger.
            app.UseSwagger();
            
            // Habilita o middleware responsável por gerar a UI do Swagger e especifica o endpoint para servir o JSON. 
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC .NET Core + Swagger V1");
                c.RoutePrefix = string.Empty; // Faz com que o swagger seja a página que é inicializada quando é executado a API.
            });

            app.UseMvc();
        }
    }
}
