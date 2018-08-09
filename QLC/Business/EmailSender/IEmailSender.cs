using System.Threading.Tasks;

namespace QLC.Business.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
