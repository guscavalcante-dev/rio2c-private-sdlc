using HtmlAgilityPack;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace PlataformaRio2C.Application.Services
{
    public class ErrorMessageService : IErrorMessageService
    {
        private readonly IEmailAppService _emailAppService;

        public ErrorMessageService(IEmailAppService emailAppService)
        {
            _emailAppService = emailAppService;
        }

        public void SendEmailException(Exception ex, string info)
        {
            //var subject = "Rio2C - Exceção não tratada";

            //var email = _systemParameterRepository.Get<string>(SystemParameterCodes.EmailExceptionSender);

            //var message = CompileHtmlMessageInvitationToCollaborator();

            //string ipValue = null;
            //string hostName = null;

            //try
            //{
            //    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            //    var hostValue = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            //    ipValue = hostValue.ToString();
            //    hostName = host.HostName;
            //}
            //catch (Exception)
            //{
                
            //}

          


            //message = message.Replace("@{Message}", "<h2>Ocorreu um exceção não tratada [{0}]</h2><div>{1}</div><p>Source: {2}</p> <p>Ip: {3}</p> <p>HostName: {4}</p> <p>Message: {5}</p> <p>StackTrace: </br> <pre style='background: #ffffcc'>{6}</pre></p>");
            //message = string.Format(message,  DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), info, ex.Source, ipValue, hostName, ex.Message, ex.StackTrace);

            //_emailAppService.SeendEmailTemplateDefault(email, subject, message, false, false);
        }


        private string CompileHtmlMessageInvitationToCollaborator()
        {
            HtmlDocument template = new HtmlDocument();

            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var pathTemplate = string.Format("{0}/TemplatesEmail/defaultTemplate.html", currentPath);

            template.Load(pathTemplate);
            return template.DocumentNode.InnerHtml;
        }
    }
}
