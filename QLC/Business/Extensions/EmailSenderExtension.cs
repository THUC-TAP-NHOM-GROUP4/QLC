using QLC.Business.EmailSender;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace QLC.Business.Extensions
{
    public static class EmailSenderExtension
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
