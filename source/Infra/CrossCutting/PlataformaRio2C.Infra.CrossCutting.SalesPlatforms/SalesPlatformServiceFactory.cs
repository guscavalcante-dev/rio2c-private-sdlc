// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SalesPlatformServiceFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using System;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
{
    /// <summary>SalesPlatformServiceFactory</summary>
    public class SalesPlatformServiceFactory : ISalesPlatformServiceFactory
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformServiceFactory"/> class.</summary>
        public SalesPlatformServiceFactory()
        {
        }

        /// <summary>
        /// Gets the specified sales platform webhook request dto.
        /// </summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException">Unknown sales platform on webhook request.</exception>
        public ISalesPlatformService Get(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            if (salesPlatformWebhookRequestDto?.SalesPlatformDto?.Name == SalePlatformName.Eventbrite)
            {
                return new EventbriteSalesPlatformService(salesPlatformWebhookRequestDto);
            }

            if (salesPlatformWebhookRequestDto?.SalesPlatformDto?.Name == SalePlatformName.Inti)
            {
               return new IntiSalesPlatformService(salesPlatformWebhookRequestDto);
            }

            if (salesPlatformWebhookRequestDto?.SalesPlatformDto?.Name == SalePlatformName.Sympla)
            {
                return new SymplaSalesPlatformService(salesPlatformWebhookRequestDto);
            }

            throw new DomainException("Unknown sales platform on webhook request.");
        }

        /// <summary>
        /// Gets the specified sales platform service by name.
        /// </summary>
        /// <param name="salesPlatformDto">The sales platform dto.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException">Unknown sales platform on webhook request.</exception>
        public ISalesPlatformService Get(SalesPlatformDto salesPlatformDto, ISalesPlatformWebhookRequestRepository salesPlatformWebhookRequestRepository)
        {
            if (salesPlatformDto?.Name == SalePlatformName.Eventbrite)
            {
                return new EventbriteSalesPlatformService(salesPlatformDto);
            }

            if (salesPlatformDto?.Name == SalePlatformName.Inti)
            {
                return new IntiSalesPlatformService(salesPlatformDto);
            }

            if (salesPlatformDto?.Name == SalePlatformName.Sympla)
            {
                return new SymplaSalesPlatformService(salesPlatformDto, salesPlatformWebhookRequestRepository);
            }

            throw new DomainException("Unknown sales platform on webhook request.");
        }
    }
}