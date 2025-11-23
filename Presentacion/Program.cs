
using Aplicacion.Interface;
using Aplicacion.Service;
using AutoMapper;
using Infraestructura.Correos;
using Infraestructura.Persistencia.Contexto;
using Infraestructura.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Presentacion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repositorio<>));
            builder.Services.AddScoped<ReservasService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<InscripcionService>();

            builder.Services.AddScoped<IinscripcionRepositorio, InscripcionRepositorio>();

            builder.Services.AddScoped<ICorreos>(sp => new Correos("ahleandro18@gmail.com"));


            builder.Services.AddSqlServer<Contexto>(builder.Configuration.GetConnectionString("StringConection"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
