using Microsoft.EntityFrameworkCore;
using SmartMedical.Infrastructure.Persistence;
using SmartMedical.Application.Interfaces;
using SmartMedical.Application.Services;
using SmartMedical.Domain.Interfaces;
using SmartMedical.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// inyeccion de repositorios
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// inyeccion de servicios
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<PriorityService>();

// Configuracion de CORS (Aceptar todo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// configuracion de entity framework core con sql server
builder.Services.AddDbContext<SmartMedicalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// configuracion del pipeline de solicitud http
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
       
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartMedical API");

        
        options.RoutePrefix = string.Empty;
    });
}

 app.UseHttpsRedirection(); 
app.UseCors("AllowAll"); // <-- Activa el CORS que configuramos arriba
app.MapControllers();



app.Run();