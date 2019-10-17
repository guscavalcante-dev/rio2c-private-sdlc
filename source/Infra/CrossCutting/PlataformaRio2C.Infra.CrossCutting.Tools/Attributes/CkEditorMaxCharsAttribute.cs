// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 10-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-17-2019
// ***********************************************************************
// <copyright file="CkEditorMaxCharsAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Helpers;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>CkEditorMaxCharsAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CkEditorMaxCharsAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int length;

        /// <summary>Initializes a new instance of the <see cref="CkEditorMaxCharsAttribute"/> class.</summary>
        /// <param name="maxChars">The maximum chars.</param>
        public CkEditorMaxCharsAttribute(int maxChars)
        {
            this.length = maxChars;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueProperty = validationContext.ObjectType.GetProperty("Value");
            var htmlText = (string)valueProperty?.GetValue(validationContext.ObjectInstance, null);

            if (HtmlutilityHelper.CountChars(htmlText) > this.length)
            {
                return new ValidationResult(this.ErrorMessage ?? string.Format(Messages.PropertyBetweenLengths, string.Empty, this.length, 1));
            }

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
                ValidationType = "ckeditormaxchars",
                ErrorMessage = this.ErrorMessage ?? string.Format(Messages.PropertyBetweenLengths, string.Empty, this.length, 1)
            };

            rule.ValidationParameters.Add("maxchars", this.length);

            yield return rule;
        }
    }
}