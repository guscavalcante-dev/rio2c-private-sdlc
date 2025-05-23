﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-17-2019
// ***********************************************************************
// <copyright file="ForgotPasswordViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    /// <summary>ForgotPasswordViewModel</summary>
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }
    }
}