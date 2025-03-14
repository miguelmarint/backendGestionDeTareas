using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar Entity Framework Core con base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskDB"));

// Agregar soporte para controladores
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // 👈 Permitir Angular
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configuración de Swagger para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger solo en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

app.Run();
