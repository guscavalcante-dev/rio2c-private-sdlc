// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Renan Valentim
// Created          : 09-28-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-28-2024
// ***********************************************************************
// <copyright file="ToPascalCaseAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    /// <summary>
    /// This attribute is applied to properties to ensure their values are converted to PascalCase.
    /// <para>
    /// IMPORTANT: It can only be applied to properties with a PUBLIC or PROTECTED setter.
    /// </para>
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ToPascalCaseAttribute : Attribute
    {
    }
}