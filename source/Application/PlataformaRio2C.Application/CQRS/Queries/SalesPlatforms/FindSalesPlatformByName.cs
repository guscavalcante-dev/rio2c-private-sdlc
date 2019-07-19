// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="FindSalesPlatformByName.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.ViewModels;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindSalesPlatformByName</summary>
    public class FindSalesPlatformByName : IRequest<SalesPlatformBaseViewModel>
    {
        public string Name { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindSalesPlatformByName"/> class.</summary>
        /// <param name="name">The name.</param>
        public FindSalesPlatformByName(string name)
        {
            this.Name = name;
        }
    }
}