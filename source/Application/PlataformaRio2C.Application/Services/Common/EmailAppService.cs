// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="EmailAppService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Net.Mail;
using System.Net.Mime;
using PlataformaRio2C.Application.Interfaces.Services;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;

namespace PlataformaRio2C.Application.Services
{
    /// <summary>EmailAppService</summary>
    public class EmailAppService: IEmailAppService
    {
        /// <summary>Initializes a new instance of the <see cref="EmailAppService"/> class.</summary>
        public EmailAppService()
        {
        }

        public bool SeendEmailTemplateDefault(string email, string subject, string message, bool enableMock, bool enableHiddenCopy)
        {
            //return _SeendEmailTemplateDefault(email, subject, message, enableMock, enableHiddenCopy, attachment);
            return true;
        }


        public bool SeendEmailTemplateDefault(string email, string subject, string message, Attachment attachment)
        {
            return _SeendEmailTemplateDefault(email, subject, message, true, true, attachment);
            //return true;
        }

        public bool SeendEmailTemplateDefault(string email, string subject, string message)
        {
            //return _SeendEmailTemplateDefault(email, subject, message, true, true, null);
            return true;
        }

        private bool _SeendEmailTemplateDefault(string email, string subject, string message, bool enableMock, bool enableHiddenCopy, Attachment attachment)
        {

            try
            {
                var currentPath = AppDomain.CurrentDomain.BaseDirectory;
                var recipient = email;

                //if (enableMock && _systemParameterRepository.Get<bool>(SystemParameterCodes.MockEnableRecipientSendEmail))
                //{
                //    recipient = _systemParameterRepository.Get<string>(SystemParameterCodes.MockRecipientSendEmail);
                //}

                //MailMessage mail = new MailMessage(_systemParameterRepository.Get<string>(SystemParameterCodes.SmtpDefaultSenderEmail), recipient);

                //var hiddenCopy = _systemParameterRepository.Get<string>(SystemParameterCodes.EmailHiddenCopySender);

                //if (enableHiddenCopy && hiddenCopy != null && !string.IsNullOrWhiteSpace(hiddenCopy))
                //{
                //    var senders = hiddenCopy.Split(';');

                //    foreach (var sender in senders)
                //    {
                //        MailAddress bcc = new MailAddress(sender);
                //        mail.Bcc.Add(bcc);
                //    }
                //}

                //mail.Subject = subject;
                //mail.IsBodyHtml = _systemParameterRepository.Get<bool>(SystemParameterCodes.SmtpIsBodyHtml);

                //if (attachment != null)
                //{
                //    mail.Attachments.Add(attachment);
                //}

                //LinkedResource inlineHeader1 = new LinkedResource(string.Format("{0}/TemplatesEmail/img/header_email_1.jpg", currentPath), MediaTypeNames.Image.Jpeg);
                //inlineHeader1.ContentId = "ContentIdImgHeader1";
                //inlineHeader1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                //AlternateView alternateView = AlternateView.CreateAlternateViewFromString(message, new System.Net.Mime.ContentType("text/html"));
                //alternateView.LinkedResources.Add(inlineHeader1);

                //mail.AlternateViews.Add(alternateView);

                //mail.Priority = MailPriority.Normal;

                //SmtpClient client = new SmtpClient();
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;

                //client.Host = _systemParameterRepository.Get<string>(SystemParameterCodes.SmtpHost);
                //client.Port = _systemParameterRepository.Get<int>(SystemParameterCodes.SmtpPort);
                //client.EnableSsl = _systemParameterRepository.Get<bool>(SystemParameterCodes.SmtpEnableSsl);

                //if (!client.UseDefaultCredentials)
                //{
                //    client.Credentials = new System.Net.NetworkCredential(_systemParameterRepository.Get<string>(SystemParameterCodes.SmtpCredentialUser), _systemParameterRepository.Get<string>(SystemParameterCodes.SmtpCredentialPass));
                //}

                //client.UseDefaultCredentials = _systemParameterRepository.Get<bool>(SystemParameterCodes.SmtpUseDefaultCredentials);

                //client.Send(mail);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

            //return true;
        }
    }
}
