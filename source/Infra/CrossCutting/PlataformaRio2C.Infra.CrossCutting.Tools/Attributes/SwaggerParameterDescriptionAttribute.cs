// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Renan Valentim
// Created          : 10-20-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-20-2023
// ***********************************************************************
// <copyright file="SwaggerParameterDescriptionAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>SwaggerParameterDescriptionAttribute</summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = true)]
    public class SwaggerParameterDescriptionAttribute : Attribute
    {
        public string Example { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerParameterDescriptionAttribute" /> class.
        /// </summary>
        /// <param name="example">The example.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public SwaggerParameterDescriptionAttribute(string example, bool isRequired = false)
        {
            this.Example = example;
            this.IsRequired = isRequired;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerParameterDescriptionAttribute" /> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="example">The example.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        public SwaggerParameterDescriptionAttribute(string description, string example = "", bool isRequired = false)
        {
            this.Description = description;
            this.Example = example;
            this.IsRequired = isRequired;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            string description = "";

            if (!string.IsNullOrEmpty(this.Description))
            {
                description = this.Description;
            }
            if (!string.IsNullOrEmpty(this.Example))
            {
                description += $@"<br/><b>Ex.: {this.Example};</b>";
            }
            if (this.IsRequired)
            {
                description += "<br/>(required)";
            }

            return description;
        }
    }
}