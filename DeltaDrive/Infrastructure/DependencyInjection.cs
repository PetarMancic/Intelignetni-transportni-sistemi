
using Application.Service_Interfaces;
using Application.Services.Interfaces;
using Core.Repository_Interfaces;
using DeltaDrive.Helpers;
using DeltaDrive.Repository;
using DeltaDrive.Repository.Interfaces;
using Infrastructure.Repository_Implementations;
using Infrastructure.Services;
using Infrastructure.Services_Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<DeltaDriveDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRideRepository, RideRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            // Services
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IHelperMethods, HelperMethods>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpClient<GeoapifyService>();

            // Identity
            services.AddScoped<IPasswordHasher<Passenger>, PasswordHasher<Passenger>>();


            return services;
        }
    }
}
