using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
