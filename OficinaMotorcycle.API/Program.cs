using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.CrossCutting.DependencyInjection;
using OficinaMotocenter.Persistence.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Registers services in the IoC container.
builder.Services.RegisterServices(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

// Configures Serilog for logging
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Aplica as migrações automaticamente ao subir a aplicação
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();  // Aplica as migrações pendentes
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();
