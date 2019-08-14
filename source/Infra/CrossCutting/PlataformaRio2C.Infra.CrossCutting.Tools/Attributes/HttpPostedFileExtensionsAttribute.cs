// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 08-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-14-2019
// ***********************************************************************
// <copyright file="HttpPostedFileExtensionsAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>
    /// HttpPostedFileExtensionsAttribute
    /// </summary>
    public class HttpPostedFileExtensionsAttribute : DataTypeAttribute, IClientValidatable
    {
        private readonly FileExtensionsAttribute _innerAttribute = new FileExtensionsAttribute();

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpPostedFileExtensionsAttribute" /> class.
        /// </summary>
        public HttpPostedFileExtensionsAttribute()
            : base(DataType.Upload)
        {
        }

        /// <summary>
        ///     Gets or sets the file name extensions.
        /// </summary>
        /// <returns>
        ///     The file name extensions, or the default file extensions (".png", ".jpg", ".jpeg", and ".gif") if the property is not set.
        /// </returns>
        public string Extensions
        {
            get { return _innerAttribute.Extensions; }
            set { _innerAttribute.Extensions = value; }
        }

        /// <summary>
        /// When implemented in a class, returns client validation rules for that class.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "extension",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            rule.ValidationParameters["extension"] = _innerAttribute.Extensions;
            yield return rule;
        }

        /// <summary>
        ///     Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <returns>
        ///     The formatted error message.
        /// </returns>
        /// <param name="name">The name of the field that caused the validation failure.</param>
        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null)
                return _innerAttribute.FormatErrorMessage(name);

            return string.Format(ErrorMessageString, name);
        }

        /// <summary>
        ///     Checks that the specified file name extension or extensions is valid.
        /// </summary>
        /// <returns>
        ///     true if the file name extension is valid; otherwise, false.
        /// </returns>
        /// <param name="value">A comma delimited list of valid file extensions.</param>
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file != null)
            {
                return _innerAttribute.IsValid(file.FileName);
            }

            return _innerAttribute.IsValid(value);
        }
    }
}