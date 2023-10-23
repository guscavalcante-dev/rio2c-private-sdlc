// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Renan Valentim
// Created          : 10-20-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-20-2023
// ***********************************************************************
// <copyright file="SwaggerParameterExampleAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>SwaggerParameterExampleAttribute</summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = true)]
    public class SwaggerParameterExampleAttribute : Attribute
    {
        public SwaggerParameterExampleAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public SwaggerParameterExampleAttribute(string value)
        {
            Name = value;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}