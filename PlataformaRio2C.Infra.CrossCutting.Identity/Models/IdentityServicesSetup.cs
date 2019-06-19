using System.Net;
using System.Net.Mail;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    public class IdentityServicesSetup
    {
        public IdentityEmailSetup EmailSetup { get; set; }
        public IdentitySmsSetup SmsSetup { get; set; }
    }

    public class IdentityEmailSetup
    {
        public bool UsesCredentials { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public NetworkCredential Credential { get; set; }
        public MailAddress From { get; set; }
        public string TwoFactorProviderSubject { get; set; }
        /// <summary>
        /// Formato da mensagem que será enviada, é necessário a variavel {0} para que
        /// o identity substitua pelo token
        /// Ex: "Seu código de segurança é: {0}"
        /// </summary>
        public string TwoFactorProviderMessageFormat { get; set; }
    }

    public class IdentitySmsSetup
    {
        /// <summary>
        /// Trata de um url de envio de sms, onde são obrigatórios duas variaveis:
        /// {0} para o telefone e {1} para o texto (body)
        /// </summary>
        public string UrlGatewaySms { get; set; }
        /// <summary>
        /// Formato da mensagem que será enviada, é necessário a variavel {0} para que
        /// o identity substitua pelo token
        /// Ex: "Seu código de segurança é: {0}"
        /// </summary>
        public string TwoFactorProviderBodyFormat { get; set; }
    }
}
