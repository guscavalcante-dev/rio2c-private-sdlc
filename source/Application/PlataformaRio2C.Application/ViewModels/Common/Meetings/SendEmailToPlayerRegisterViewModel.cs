
// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-26-2021
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-25-2025
// ***********************************************************************
// <copyright file="SendEmailToPlayerRegisterViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>SendEmailToPlayerRegisterViewModel</summary>
    public class SendEmailToPlayerRegisterViewModel
    {
        public Guid NegotiationUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SendEmailToPlayerRegisterViewModel"/> class.</summary>
        public SendEmailToPlayerRegisterViewModel()
        {
        }
    }
}