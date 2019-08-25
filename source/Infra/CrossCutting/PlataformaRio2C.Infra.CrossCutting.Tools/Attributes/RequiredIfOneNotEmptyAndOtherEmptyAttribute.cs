// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 08-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2019
// ***********************************************************************
// <copyright file="RequiredIfOneNotEmptyAndOtherEmptyAttribute.cs" company="Softo">
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
    /// <summary>RequiredIfOneNotEmptyAndOtherEmptyAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredIfOneNotEmptyAndOtherEmptyAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string dependentPropertyNotEmpty;
        private readonly string dependentPropertyEmpty;

        /// <summary>Initializes a new instance of the <see cref="RequiredIfOneNotEmptyAndOtherEmptyAttribute"/> class.</summary>
        /// <param name="dependentPropertyNotEmpty">The dependent property not empty.</param>
        /// <param name="dependentPropertyEmpty">The dependent property empty.</param>
        /// <param name="errorMessage">The error message.</param>
        public RequiredIfOneNotEmptyAndOtherEmptyAttribute(string dependentPropertyNotEmpty, string dependentPropertyEmpty, string errorMessage)
            : base(errorMessage)
        {
            this.dependentPropertyNotEmpty = dependentPropertyNotEmpty;
            this.dependentPropertyEmpty = dependentPropertyEmpty;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Validate first property (should be not empty to continue validation)
            var propertyNotEmptyObject = validationContext.ObjectType.GetProperty(this.dependentPropertyEmpty);
            var propertyNotEmptyObjectValue = propertyNotEmptyObject?.GetValue(validationContext.ObjectInstance, null);
            if (propertyNotEmptyObjectValue == null)
            {
                return ValidationResult.Success;
            }

            // Validate second property
            var propertyEmptyObject = validationContext.ObjectType.GetProperty(this.dependentPropertyEmpty);
            var propertyEmptyObjectValue = propertyEmptyObject?.GetValue(validationContext.ObjectInstance, null);
            if (propertyEmptyObjectValue == null && value == null)
            {
                //return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
                return new ValidationResult(FormatErrorMessage(null));
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
                ValidationType = "requiredifonenotemptyandotherempty",
                ErrorMessage = this.ErrorMessage ?? string.Format(Messages.TheFieldIsRequired, metadata.GetDisplayName())
            };

            rule.ValidationParameters.Add("dependentpropertynotempty", this.dependentPropertyNotEmpty);
            rule.ValidationParameters.Add("dependentpropertyempty", this.dependentPropertyEmpty);

            yield return rule;
        }
    }
}