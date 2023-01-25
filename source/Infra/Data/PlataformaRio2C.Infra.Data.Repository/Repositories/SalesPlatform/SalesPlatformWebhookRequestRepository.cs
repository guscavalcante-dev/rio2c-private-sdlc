// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 07-11-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequestRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Sales Platform Webhook Request IQueryable Extensions

    /// <summary>
    /// SalesPlatformWebhooRequestIQueryableExtensions
    /// </summary>
    internal static class SalesPlatformWebhooRequestIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformWebhookRequestUid">The sales platform webhook request uid.</param>
        /// <returns></returns>
        internal static IQueryable<SalesPlatformWebhookRequest> FindByUid(this IQueryable<SalesPlatformWebhookRequest> query, Guid salesPlatformWebhookRequestUid)
        {
            query = query.Where(spwr => spwr.Uid == salesPlatformWebhookRequestUid);

            return query;
        }

        /// <summary>Determines whether this instance is pending.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<SalesPlatformWebhookRequest> IsPending(this IQueryable<SalesPlatformWebhookRequest> query)
        {
            query = query.Where(spwr => !spwr.IsProcessed
                                        && !spwr.IsProcessing
                                        && spwr.ProcessingCount <= 15
                                        && spwr.NextProcessingDate < DateTime.UtcNow);

            return query;
        }

        /// <summary>
        /// Finds the by payload contains.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformAttendeeIds">The sales platform attendee ids.</param>
        /// <returns></returns>
        internal static IQueryable<SalesPlatformWebhookRequest> FindByPayloadContains(this IQueryable<SalesPlatformWebhookRequest> query, string[] salesPlatformAttendeeIds)
        {
            if (salesPlatformAttendeeIds?.Length > 0)
            {
                var outerWhere = PredicateBuilder.New<SalesPlatformWebhookRequest>(false);

                foreach (var salesPlatformAttendeeId in salesPlatformAttendeeIds)
                {
                    outerWhere = outerWhere.Or(p => p.Payload.Contains(salesPlatformAttendeeId));
                }

                query = query.Where(outerWhere).AsQueryable();
            }

            return query;
        }

        /// <summary>
        /// Finds the by sale platform identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformId">The sales platform identifier.</param>
        /// <returns></returns>
        internal static IQueryable<SalesPlatformWebhookRequest> FindBySalePlatformId(this IQueryable<SalesPlatformWebhookRequest> query, int salesPlatformId)
        {
            query = query.Where(spwr => spwr.SalesPlatformId == salesPlatformId);

            return query;
        }
    }

    #endregion

    /// <summary>SalesPlatformWebhookRequestRepository</summary>
    public class SalesPlatformWebhookRequestRepository : Repository<PlataformaRio2CContext, SalesPlatformWebhookRequest>, ISalesPlatformWebhookRequestRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequestRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public SalesPlatformWebhookRequestRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<SalesPlatformWebhookRequest> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet;

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Gets all by pending asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<SalesPlatformWebhookRequest>> FindAllByPendingAsync()
        {
            var query = this.GetBaseQuery()
                                .IsPending();

            return await query.ToListAsync();
        }

        /// <summary>Finds all dto by pending dto asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<SalesPlatformWebhookRequestDto>> FindAllDtoByPendingAsync()
        {
            var query = this.GetBaseQuery()
                                .IsPending()
                                .OrderBy(spwr => spwr.CreateDate)
                                .Select(spwr => new SalesPlatformWebhookRequestDto
                                {
                                    Uid = spwr.Uid,
                                    SalesPlatformWebhookRequest = spwr,
                                    SalesPlatformDto = new SalesPlatformDto
                                    {
                                        Uid = spwr.SalesPlatform.Uid,
                                        Name = spwr.SalesPlatform.Name,
                                        WebhookSecurityKey = spwr.SalesPlatform.WebhookSecurityKey,
                                        ApiKey = spwr.SalesPlatform.ApiKey,
                                        ApiSecret = spwr.SalesPlatform.ApiSecret,
                                        MaxProcessingCount = spwr.SalesPlatform.MaxProcessingCount,
                                        CreationDate = spwr.SalesPlatform.CreateDate,
                                        UpdateUserId = spwr.SalesPlatform.UpdateUserId,
                                        UpdateDate = spwr.SalesPlatform.UpdateDate,
                                        SecurityStamp = spwr.SalesPlatform.SecurityStamp,
                                    }
                                });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all dto by sales platform identifier asynchronous.
        /// </summary>
        /// <param name="salesPlatformId">The sales platform identifier.</param>
        /// <returns></returns>
        public async Task<List<SalesPlatformWebhookRequestDto>> FindAllDtoBySalesPlatformIdAsync(int salesPlatformId)
        {
            var query = this.GetBaseQuery()
                                .FindBySalePlatformId(salesPlatformId)
                                .OrderBy(spwr => spwr.CreateDate)
                                .Select(spwr => new SalesPlatformWebhookRequestDto
                                {
                                    Uid = spwr.Uid,
                                    Payload = spwr.Payload
                                });

            return await query.ToListAsync();
        }
    }
}