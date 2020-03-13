// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 09-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="RadioButtonRequiredIfAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>RadioButtonRequiredIfAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RadioButtonRequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string dependentProperty;
        private readonly string dependentPropertyValue;

        /// <summary>Initializes a new instance of the <see cref="RadioButtonRequiredIfAttribute"/> class.</summary>
        /// <param name="dependentProperty">The dependent property.</param>
        /// <param name="dependentPropertyValue">The dependent property value.</param>
        /// <param name="errorMessage">The error message.</param>
        public RadioButtonRequiredIfAttribute(string dependentProperty, string dependentPropertyValue, string errorMessage = null)
            : base(errorMessage)
        {
            this.dependentProperty = dependentProperty;
            this.dependentPropertyValue = dependentPropertyValue;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Validate first property (should be not empty to continue validation)
            var property = validationContext.ObjectType.GetProperty(this.dependentProperty);
            var propertyValue = property?.GetValue(validationContext.ObjectInstance, null);
            if (propertyValue == null)
            {
                return ValidationResult.Success;
            }

            if (propertyValue.ToString().ToLowerInvariant() != this.dependentPropertyValue.ToLowerInvariant())
            {
                return ValidationResult.Success;
            }

            if (value != null)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format(Messages.TheFieldIsRequired, validationContext.DisplayName));
        }

        /// <summary>When implemented in a class, returns client validation rules for that class.</summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>The client validation rules for this validator.</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "radiobuttonrequiredif",
                ErrorMessage = this.ErrorMessage ?? string.Format(Messages.TheFieldIsRequired, metadata.GetDisplayName())
            };

            rule.ValidationParameters.Add("dependentproperty", this.dependentProperty);
            rule.ValidationParameters.Add("dependentvalue", this.dependentPropertyValue);

            yield return rule;
        }
    }
}