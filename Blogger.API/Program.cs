using Blogger.API.Middlewares;
using Blogger.Application.DependencyInjection;
using Blogger.Database.DependencyInjection;

namespace Blogger.API;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddDatabase(builder.Configuration)
            .AddApplication()
            .AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

        app.UseExceptionHandler(options => { });
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}