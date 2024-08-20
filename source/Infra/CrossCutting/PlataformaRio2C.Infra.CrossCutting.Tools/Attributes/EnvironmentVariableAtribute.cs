// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Elton Assuncao
// Created          : 08-20-2024
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="EnvironmentVariableAttribute" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]

    public class EnvironmentVariableAttribute : Attribute
    {
        public EnumEnvironments[] Name { get; set; }

        public EnvironmentVariableAttribute(EnumEnvironments[] name)
        {
            this.Name = name;
        }
    }
}
