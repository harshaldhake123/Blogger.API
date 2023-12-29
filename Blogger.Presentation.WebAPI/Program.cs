using Blogger.Domain.Core.DependencyInjection;
using Blogger.Infrastructure.Database.DependencyInjection;
using Blogger.Presentation.WebAPI.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddDomain()
    .AddSingleton<IApplicationUserService, ApplicationUserService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();