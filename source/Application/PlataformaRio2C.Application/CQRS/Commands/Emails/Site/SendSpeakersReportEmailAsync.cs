// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-27-2023
// ***********************************************************************
// <copyright file="SendSpeakersReportEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendSpeakersReportEmailAsync</summary>
    public class SendSpeakersReportEmailAsync : EmailBaseCommand
    {
        public string FilePath { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendSpeakersReportEmailAsync" /> class.
        /// </summary>
        /// <param name="filePath">Name of the file.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendSpeakersReportEmailAsync(
            string filePath,
            string recipientEmail, 
            Edition edition,
            string userInterfaceLanguage)
            : base(recipientEmail, edition, userInterfaceLanguage)
        {
            this.FilePath = filePath;
        }
    }
}