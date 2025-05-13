// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="FindAllSalesPlatformWebhooRequestsDtoByPending.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllSalesPlatformWebhooRequestsDtoByPending</summary>
    public class FindAllSalesPlatformWebhooRequestsDtoByPending : IRequest<List<SalesPlatformWebhookRequestDto>>
    {
        /// <summary>Initializes a new instance of the <see cref="FindAllSalesPlatformWebhooRequestsDtoByPending"/> class.</summary>
        public FindAllSalesPlatformWebhooRequestsDtoByPending()
        {
        }
    }
}