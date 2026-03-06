namespace Application.Service_Interfaces
{
    public interface  IEmailService
    {
        Task SendVerificationEmailAsync(string toEmail, string firstName, string token);
    }
}
