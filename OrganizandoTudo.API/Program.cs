using Microsoft.Extensions.Options;
using OrganizandoTudo.Infrastructure.Data.MongoDB.Configuration;
using OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces;
using OrganizandoTudo.Infrastructure.Data.MongoDB.Configuration.Repositories;
using OrganizandoTudo.Domain.Interfaces.Repositories;

namespace OrganizandoTudo.API
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

            // Configuração MongoDB
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            // Registros de Dependência
            builder.Services.AddScoped<IMongoContext, MongoDbContext>();
            builder.Services.AddScoped<IUserRepository, UserMongoRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
