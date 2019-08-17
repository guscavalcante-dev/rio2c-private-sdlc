// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="BaseQuery.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>BaseQuery</summary>
    public class BaseQuery<T> : IRequest<T>
    {
        public string UserInterfaceLanguage { get; private set; }

        /// <summary>Updates the base properties.</summary>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdateBaseProperties(string userInterfaceLanguage)
        {
            this.UserInterfaceLanguage = userInterfaceLanguage;
        }
    }
}