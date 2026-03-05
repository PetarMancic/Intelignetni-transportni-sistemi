using Application;
using Application.Settings;
using DeltaDrive.HubSimulation;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings!.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();


builder.Services.AddInfrastructure(builder.Configuration);

//builder.Services.AddDbContext<DeltaDriveDbContext>(options =>
//    options.UseNpgsql(
//        builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly); // Application assembly
});

//builder.Services.AddScoped<IPasswordHasher<Passenger>, PasswordHasher<Passenger>>();
//builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
//builder.Services.AddScoped<IVehicleService, VehicleService>();
//builder.Services.AddScoped<IHelperMethods, HelperMethods>();
//builder.Services.AddScoped<IRideRepository, RideRepository>();
//builder.Services.AddScoped<IRideService, RideService>();
//builder.Services.AddScoped<IPassengerRepository,PassengerRepository>();
//builder.Services.AddHttpClient<GeoapifyService>();


builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(10);  // ping svake 10s
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30); // čekaj 30s pre disconnect
});

//builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500",
            "http://localhost:8080",      // ✅ dodaj ovo
            "https://localhost:8080",
            "http://localhost:5173",
            "https://id-preview--6c78d5ae-6d44-40ee-b3db-a3de997c4db3.lovable.app",
            "https://ride-watch-dash.lovable.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // obavezno za SignalR
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("FrontendPolicy");

app.MapHub<RideHub>("/hubs/ride");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
