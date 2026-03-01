using DeltaDrive.Helpers;
using DeltaDrive.HubSimulation;
using DeltaDrive.Repository;
using DeltaDrive.Repository.Interfaces;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Services;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DeltaDriveDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly); // Application assembly
});

builder.Services.AddScoped<IPasswordHasher<Passenger>, PasswordHasher<Passenger>>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IHelperMethods, HelperMethods>();
builder.Services.AddScoped<IRideRepository, RideRepository>();
builder.Services.AddScoped<IRideService, RideService>();
builder.Services.AddScoped<IPassengerRepository,PassengerRepository>();
builder.Services.AddHttpClient<GeoapifyService>();


builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", 
            "https://id-preview--6c78d5ae-6d44-40ee-b3db-a3de997c4db3.lovable.app",
            "https://ride-watch-dash.lovable.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // obavezno za SignalR
    });
});


var app = builder.Build();

app.UseCors("FrontendPolicy");

app.MapHub<RideHub>("/hubs/ride");

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
