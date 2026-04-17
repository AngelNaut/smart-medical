using Microsoft.EntityFrameworkCore;
using SmartMedical.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<SmartMedicalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.MapControllers();



app.Run();