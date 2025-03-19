
using Microsoft.EntityFrameworkCore;
using SalesApi.Application.Services;
using SalesApi.Domain.Interfaces;
using SalesApi.Infrastructure.Database;
using SalesApi.Infrastructure.Messaging;
using System.Reflection;

namespace SalesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<SalesDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("SalesApiDb")));

            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register application services.
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            builder.Services.AddScoped<IEventPublisher, EventPublisher>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
                dbContext.Database.Migrate();
            }

            app.UseHealthChecks("/health");

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
