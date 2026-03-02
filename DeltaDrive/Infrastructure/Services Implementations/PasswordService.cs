using Application.Service_Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<Passenger> _hasher;

        public PasswordService(IPasswordHasher<Passenger> hasher)
        {
            _hasher = hasher;
        }

        public string HashPassword(string password) =>
            _hasher.HashPassword(null, password);

      
    }
}
