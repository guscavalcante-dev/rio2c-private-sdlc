using System;
// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           :  Elton Assunção
// Created          : 01-04-2024
//
// Last Modified By : Elton Assunção
// Last Modified On : 01-04-2024
// ***********************************************************************
// <copyright file="SwaggerDefaultValueAttribute.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    public class SwaggerDefaultValueAttribute : Attribute
    {
        public SwaggerDefaultValueAttribute(object value)
        {
            Value = value;          
        }
        
        public object Value { get; set; }    
    }
}