// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="FindSalesPlatformDtoByName.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindSalesPlatformDtoByName</summary>
    public class FindSalesPlatformDtoByName : IRequest<SalesPlatformDto>
    {
        public string Name { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindSalesPlatformDtoByName"/> class.</summary>
        /// <param name="name">The name.</param>
        public FindSalesPlatformDtoByName(string name)
        {
            this.Name = name;
        }
    }
}