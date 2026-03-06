using Application.Service_Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Infrastructure.Services_Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  async Task SendVerificationEmailAsync(string toEmail, string firstName, string token)
        {
            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"]!);
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPass = _configuration["Email:SmtpPass"];
            var baseUrl = _configuration["Email:BaseUrl"];

            var verifyLink = $"{baseUrl}/api/auth/verify-email?token={token}";

            var htmlBody = $"""
            <!DOCTYPE html>
            <html>
            <body style="font-family: Arial, sans-serif; background:#f4f4f4; padding:40px;">
              <div style="max-width:500px; margin:auto; background:white; 
                          border-radius:10px; padding:40px; box-shadow:0 2px 8px rgba(0,0,0,0.1)">
                <h2 style="color:#333">Zdravo, {firstName}! 👋</h2>
                <p style="color:#555">Hvala na registraciji. Klikni dugme ispod da verifikuješ svoj email:</p>
                <div style="text-align:center; margin:30px 0">
                  <a href="{verifyLink}" 
                     style="background:#4F46E5; color:white; padding:14px 32px; 
                            border-radius:8px; text-decoration:none; font-size:16px; font-weight:bold">
                    ✅ Verifikuj Email
                  </a>
                </div>
                <p style="color:#999; font-size:12px">
                  Link važi 24 sata. Ako nisi ti kreirao nalog, ignoriši ovaj mail.
                </p>
              </div>
            </body>
            </html>
            """;

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(smtpUser!, "MyApp"),
                Subject = "Verifikacija emaila",
                Body = htmlBody,
                IsBodyHtml = true
            };
            mail.To.Add(toEmail);

            await client.SendMailAsync(mail);
        }
    }
}
