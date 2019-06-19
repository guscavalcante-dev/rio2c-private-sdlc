using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class IdentityEmailMessageService : IIdentityMessageService
    {
        private readonly IdentityEmailSetup _setup;
        public IdentityEmailMessageService(IdentityEmailSetup setup)
        {
            _setup = setup;
        }

        public Task SendAsync(IdentityMessage message)
        {
            var text = HttpUtility.HtmlEncode(message.Body);
            var msg = new MailMessage
            {
                From = new MailAddress(_setup.From.Address, _setup.From.DisplayName),
                IsBodyHtml = _setup.IsBodyHtml,
                Body = message.Body,
                Subject = message.Subject,
            };

            msg.To.Add(new MailAddress(message.Destination));
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

            var smtpClient = new SmtpClient(_setup.Host, _setup.Port);
            if (_setup.UsesCredentials && _setup.Credential != null)
            {
                smtpClient.Credentials = _setup.Credential;
                smtpClient.EnableSsl = true;
            }


            smtpClient.Send(msg);
            return Task.FromResult(0);
        }
    }
}
