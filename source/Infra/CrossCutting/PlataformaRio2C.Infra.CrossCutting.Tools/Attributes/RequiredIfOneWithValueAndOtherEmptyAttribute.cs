// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 09-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="RequiredIfOneWithValueAndOtherEmptyAttribute.cs" company="Softo">
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
    /// <summary>RequiredIfOneWithValueAndOtherEmptyAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredIfOneWithValueAndOtherEmptyAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string dependentPropertyWithValue;
        private readonly string dependentPropertyValue;
        private readonly string dependentPropertyEmpty;

        public RequiredIfOneWithValueAndOtherEmptyAttribute(string dependentPropertyWithValue, string dependentPropertyValue, string dependentPropertyEmpty, string errorMessage = null)
            : base(errorMessage)
        {
            this.dependentPropertyWithValue = dependentPropertyWithValue;
            this.dependentPropertyValue = dependentPropertyValue;
            this.dependentPropertyEmpty = dependentPropertyEmpty;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Validate first property (should be not empty to continue validation)
            var propertyWithValue = validationContext.ObjectType.GetProperty(this.dependentPropertyWithValue);
            var propertyWithValueValue = propertyWithValue?.GetValue(validationContext.ObjectInstance, null);
            if (propertyWithValueValue == null)
            {
                return ValidationResult.Success;
            }

            if (propertyWithValueValue.ToString().ToLowerInvariant() != this.dependentPropertyValue.ToLowerInvariant())
            {
                return ValidationResult.Success;
            }

            // Validate second property
            var propertyEmptyObject = validationContext.ObjectType.GetProperty(this.dependentPropertyEmpty);
            var propertyEmptyObjectValue = propertyEmptyObject?.GetValue(validationContext.ObjectInstance, null);
            if (propertyEmptyObjectValue == null && value == null)
            {
                return new ValidationResult(string.Format(Messages.TheFieldIsRequired, validationContext.DisplayName));
                //return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
                //return new ValidationResult(FormatErrorMessage(null));
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
                ValidationType = "requiredifonewithvalueandotherempty",
                ErrorMessage = this.ErrorMessage ?? string.Format(Messages.TheFieldIsRequired, metadata.GetDisplayName())
            };

            rule.ValidationParameters.Add("dependentpropertywithvalue", this.dependentPropertyWithValue);
            rule.ValidationParameters.Add("dependentpropertyvalue", this.dependentPropertyValue);
            rule.ValidationParameters.Add("dependentpropertyempty", this.dependentPropertyEmpty);

            yield return rule;
        }
    }
}