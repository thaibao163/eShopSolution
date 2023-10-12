using Domain.ViewModel.Emails;

namespace Persistence.Services.Emails
{
    public interface IMailService
    {
        Task<bool> ForgetPasswordSendMail(string toEmail, string username, string resetToken);

        Task SendEmailAsync(MailRequest request);
    }
}