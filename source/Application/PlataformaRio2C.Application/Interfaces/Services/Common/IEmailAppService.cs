using System.Net.Mail;

namespace PlataformaRio2C.Application.Interfaces.Services
{
    public interface IEmailAppService
    {
        bool SeendEmailTemplateDefault(string email, string subject, string message);
        bool SeendEmailTemplateDefault(string email, string subject, string message, Attachment attachment);
        bool SeendEmailTemplateDefault(string email, string subject, string message, bool enableMock, bool enableHiddenCopy);
    }
}
