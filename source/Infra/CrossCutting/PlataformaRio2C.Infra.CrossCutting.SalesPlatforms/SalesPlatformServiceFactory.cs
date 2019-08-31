// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="SalesPlatformServiceFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
{
    /// <summary>SalesPlatformServiceFactory</summary>
    public class SalesPlatformServiceFactory : ISalesPlatformServiceFactory
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformServiceFactory"/> class.</summary>
        public SalesPlatformServiceFactory()
        {
        }

        /// <summary>Gets the specified sales platform webhook request dto.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        /// <returns></returns>
        public ISalesPlatformService Get(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            if (salesPlatformWebhookRequestDto?.SalesPlatformDto?.Name == "Eventbrite")
            {
                return new EventbriteSalesPlatformService(salesPlatformWebhookRequestDto);
            }

            throw new DomainException("Unknown sales platform on webhook request.");
        }
    }
}