using AutoMapper;
using JWTSpa_Autentication.Data;
using JWTSpa_Autentication.Helpers;
using JWTSpa_Autentication.Profiles;
using JWTSpa_Autentication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueCliMiddleware;

namespace JWTSpa_Autentication
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
            //configuramos el punto de acceso para el automapper
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
            //services.AddSingleton(provider =>
            //{
            //    new MapperConfiguration(config =>
            //    {
            //        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
            //        config.AddProfile(new AutoMapperProfiles(geometryFactory));
            //    }).CreateMapper()
            //});

            services.AddDbContext<UserContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), 
                    sqlServerOptions => sqlServerOptions.UseNetTopologySuite()));

            // se configura el middleware de newtonsoft
            services.AddControllers().AddNewtonsoftJson();
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IPeliculasRepository, PeliculasRepository>();
            services.AddScoped<JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options => options 
                .WithOrigins(new[] { "http://localhost:3000", "http://localhost:8080", "http://localhost:8081", "http://localhost:8082/", "http://localhost:8086","http://localhost:4200"})
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });
        }
    }
}
