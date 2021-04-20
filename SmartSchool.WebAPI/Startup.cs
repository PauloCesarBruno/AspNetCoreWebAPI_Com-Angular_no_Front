using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebAPI
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
            services.AddDbContext<SmartContext>(context =>
                context.UseSqlServer(Configuration.GetConnectionString("Default"))
            );

            // Abaixo Ignora o Loop do Json.
            services.AddControllers()
                    .AddNewtonsoftJson(
                        opt => opt.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Injetando dependência para o Auto_Mapper Ja baixado e referenciasdo no (csproj)
            // Quero fazer Mapeamento entre os meus DTO´s e meu Dominio(Models).
            /* Passando como parâmetro a aplicação de dominio dos assemblies, para que o Auto_Mapper 
            procure dentro dos meus assmblies (Dll´s) quem esta herdando de Profille dentro da pasra
            (Helpers onde esta o SmartSchoolProfile.cs).*/
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Injetando IRepository e Repository p/ os Controller para encapsulamento e abstração do Contexto 
            services.AddScoped<IRepository, Repository>();

            // DOCUMENTAÇÃO DO SWAGGER
            // =================================================================================================
            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(opt => 
            {
                // Caso não sendo imposta uma Vesão para minha Controller, a Versão irá ser a Padrão...
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
            }); 

            var apiProvidersDescription = services.BuildServiceProvider()
                                                  .GetService<IApiVersionDescriptionProvider>();            
            services.AddSwaggerGen(opt => { 
                
            //====================================================================================================================================
            // Tem que entrar em um Foreach para pegar a String da Verssão
            // Serviço Abaixo adiciona o SWeger Instalado vide (csproj)

                foreach (var description in apiProvidersDescription.ApiVersionDescriptions)
                {
                        // No localhoste para pegar o json vai dar: (http://localhost:5000/swagger/SmartSchoolAPI/swagger.json)
                        opt.SwaggerDoc(                     
                        description.GroupName, 
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "SmartSchool API",
                            Version = description.ApiVersion.ToString(),
                            // Não é meu site de Termo de Serviços, mesmo porque não tenho um
                            // Porem servira soment para teste...
                            TermsOfService  = new Uri("http://sistemahospitalar.gear.host/"),
                            Description = "Web API para Consumo de Escolas e Cursos",
                            // Colocando licença, por eEx. Só poderá acessar a API quem tiver uma licênça especifica.
                            License = new Microsoft.OpenApi.Models.OpenApiLicense
                            {
                                Name = "SamrtSchool Licênsa",

                                // Não é um Site de Termos de Uso, porém está direcionado a ele apenas para testes de Swagger
                                Url = new Uri("https://www.youtube.com/channel/UC-7rKFVKo4JNNifPBBEEoYw")
                            },

                            Contact = new Microsoft.OpenApi.Models.OpenApiContact
                            {
                                Name = "Paulo Cesar Cordovil Bruno",
                                Email ="p_bruno001@hotmail.com",
                                Url = new Uri ("http://sistemahospitalar.gear.host/")
                            }
                        }
                    );  
                }                           
                
            // FINAL DA DOCUMENTAÇÃO DO SWAGGER
            // =================================================================================================


                // Para o Swagger
                // xml foi criado no Gerenciador de Soluções
                // Combinando o meu diretório atual com o nome do meu XML ( xmlCommentsFile).
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                opt.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddMvcCore();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IApiVersionDescriptionProvider apiProviderDescription) // Add (Injetado).
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // CONFIGURAÇÃO DO SWAGGER
            app.UseSwagger()
               //===================================================================================
               .UseSwaggerUI(opt =>
               {
                   foreach (var description in  apiProviderDescription.ApiVersionDescriptions)
                   {
                        opt.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant()
                        );                       
                   }
                   opt.RoutePrefix = "";
               });

            /*==================================================================================
            DIGITAR: http://localhost:46458/index.html NO VISUAL STUDIO 2019, por motivo que no
            Visual Studio 2019 o localhoste esta rodando no IIS na Porta Acima descrita(46458).
            Rodar o navegador primeiro edigitar a URL -> http://localhost:46458/index.html
            ===================================================================================*/

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}