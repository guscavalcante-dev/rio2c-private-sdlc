// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="IdentityProviderSetup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net;
using System.Net.Mail;
using Microsoft.Owin.Security.DataProtection;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Models
{
    /// <summary>IdentityProviderSetup</summary>
    public static class IdentityProviderSetup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
    }

    /// <summary>IdentityServicesSetup</summary>
    public class IdentityServicesSetup
    {
        public IdentityEmailSetup EmailSetup { get; set; }
        public IdentitySmsSetup SmsSetup { get; set; }
    }

    /// <summary>IdentityEmailSetup</summary>
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

    /// <summary>IdentitySmsSetup</summary>
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
