
namespace Application.Service_Interfaces
{
    // Application/Interfaces/IPasswordService.cs
    public interface IPasswordService
    {
        string HashPassword(string password);
       // bool VerifyPassword(string hashedPassword, string password);
    }
}
