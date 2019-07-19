// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequestRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>SalesPlatformWebhookRequestRepository</summary>
    public class SalesPlatformWebhookRequestRepository : Repository<PlataformaRio2CContext, SalesPlatformWebhookRequest>, ISalesPlatformWebhookRequestRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequestRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public SalesPlatformWebhookRequestRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<SalesPlatformWebhookRequest> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;
                                //.Include(i => i.SalesPlatform);

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}