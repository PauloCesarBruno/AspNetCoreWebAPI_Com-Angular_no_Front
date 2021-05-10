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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(options => 
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options => 
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();   

            services.AddSwaggerGen(options => {                 
            //====================================================================================================================================
            // Tem que entrar em um Foreach para pegar a String da Verssão
            // Serviço Abaixo adiciona o SWeger Instalado vide (csproj)

                 foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "SmartSchool API",
                            Version = description.ApiVersion.ToString(),
                            TermsOfService = new Uri(" http://sistemahospitalar.gear.host/"), // Exemplo Site Gestão de Vendas.
                            Description = "A descrição da WebAPI do SmartSchool",
                            License = new Microsoft.OpenApi.Models.OpenApiLicense
                            {
                                Name = "SmartSchool License",
                                Url = new Uri("https://www.youtube.com/channel/UC-7rKFVKo4JNNifPBBEEoYw?view_as=subscriber") // Exemplo Meu Canal YouTube
                            },
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact
                            {
                                Email = "p_bruno001@hotmail.com",
                                Name = "PAULO CESAR C. BRUNO.",
                                Url = new Uri("http://pb-techprogramas.gear.host/index.cshtml") // Exemplo Site Sistema Hospitalar.
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

                options.IncludeXmlComments(xmlCommentsFullPath);
            });
            
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
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // CONFIGURAÇÃO DO SWAGGER
            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
                   foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                   {                       
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json", 
                            description.GroupName.ToUpperInvariant());
                   }
                   options.RoutePrefix = "";
               });

            // app.UseAuthorization();


            /*==================================================================================
            DIGITAR: http://localhost:46458/index.html NO VISUAL STUDIO 2019, por motivo que no
            Visual Studio 2019 o localhoste esta rodando no IIS na Porta Acima descrita(46458).
            Rodar o navegador primeiro edigitar a URL -> http://localhost:46458/index.html
            ===================================================================================*/            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}