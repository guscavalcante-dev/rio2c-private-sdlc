// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 09-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="RequiredImageOptionalAttribute.cs" company="Softo">
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
    /// <summary>RequiredImageOptionalAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredImageOptionalAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>Initializes a new instance of the <see cref="RequiredImageOptionalAttribute"/> class.</summary>
        public RequiredImageOptionalAttribute()
        {
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isRequired = validationContext.ObjectType.GetProperty("IsRequired");
            var isRequiredValue = isRequired?.GetValue(validationContext.ObjectInstance, null);
            if (isRequiredValue?.ToString().ToLowerInvariant() != "true")
            {
                return ValidationResult.Success;
            }

            var imageFile = validationContext.ObjectType.GetProperty("ImageFile");
            var imageFileValue = imageFile?.GetValue(validationContext.ObjectInstance, null);
            if (imageFileValue != null)
            {
                return ValidationResult.Success;
            }

            var imageUploadDate = validationContext.ObjectType.GetProperty("ImageUploadDate");
            var imageUploadDateValue = imageUploadDate?.GetValue(validationContext.ObjectInstance, null);
            if (imageUploadDateValue == null)
            {
                return new ValidationResult(string.Format(Messages.TheFieldIsRequired, validationContext.DisplayName));
            }

            var isImageDeleted = validationContext.ObjectType.GetProperty("IsImageDeleted");
            var isImageDeletedValue = isImageDeleted?.GetValue(validationContext.ObjectInstance, null);
            if (isImageDeletedValue?.ToString().ToLowerInvariant() == "true")
            {
                return new ValidationResult(string.Format(Messages.TheFieldIsRequired, validationContext.DisplayName));
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
                ValidationType = "requiredimageoptional",
                ErrorMessage = this.ErrorMessage ?? string.Format(Messages.TheFieldIsRequired, metadata.GetDisplayName())
            };

            yield return rule;
        }
    }
}