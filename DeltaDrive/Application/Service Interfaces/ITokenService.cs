namespace Application.Service_Interfaces
{
    public  interface ITokenService
    {
        string  GenerateToken(int userId, string email);
    }
}
