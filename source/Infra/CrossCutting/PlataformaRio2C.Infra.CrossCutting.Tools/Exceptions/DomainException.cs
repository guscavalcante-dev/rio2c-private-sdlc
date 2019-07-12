// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-12-2019
// ***********************************************************************
// <copyright file="DomainException.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions
{
    /// <summary>DomainException</summary>
    public class DomainException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="DomainException"/> class.</summary>
        public DomainException() : base("Generic Domain Error")
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DomainException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public DomainException(string message) : base(message)
        {
        }
    }
}
