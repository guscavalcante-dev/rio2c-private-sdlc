// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-17-2019
// ***********************************************************************
// <copyright file="ResetPasswordViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.ViewModels
{
    /// <summary>ResetPasswordViewModel</summary>
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MinimumNumberOfCharacters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordConfirmationDoesNotMatch")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Labels))]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}