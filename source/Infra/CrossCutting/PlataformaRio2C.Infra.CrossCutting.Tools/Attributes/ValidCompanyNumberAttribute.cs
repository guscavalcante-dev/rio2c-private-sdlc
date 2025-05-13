// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 09-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="ValidCompanyNumberAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>ValidCompanyNumberAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidCompanyNumberAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>Returns true if ... is valid.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }

        /// <summary>When implemented in a class, returns client validation rules for that class.</summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>The client validation rules for this validator.</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "validcompanynumber",
                ErrorMessage = this.ErrorMessage ?? string.Format(Messages.InvalidCompanyNumber, metadata.GetDisplayName())
            };

            yield return rule;
        }
    }
}