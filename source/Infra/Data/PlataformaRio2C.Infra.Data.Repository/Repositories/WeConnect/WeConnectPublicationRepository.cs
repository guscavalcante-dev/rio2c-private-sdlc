// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="WeConnectPublicationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region We Connect Publication IQueryable Extensions

    /// <summary>
    /// WeConnectPublicationIQueryableExtensions
    /// </summary>
    internal static class WeConnectPublicationIQueryableExtensions
    {
        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="weConnectPublicationUid">The we connect publication uid.</param>
        /// <returns></returns>
        internal static IQueryable<WeConnectPublication> FindByUid(this IQueryable<WeConnectPublication> query, Guid weConnectPublicationUid)
        {
            query = query.Where(sp => sp.Uid == weConnectPublicationUid);

            return query;
        }

        /// <summary>
        /// Finds the by social media platform publication identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="socialMediaPlatformPublicationId">The social media platform publication identifier.</param>
        /// <returns></returns>
        internal static IQueryable<WeConnectPublication> FindBySocialMediaPlatformPublicationId(this IQueryable<WeConnectPublication> query, string socialMediaPlatformPublicationId)
        {
            query = query.Where(sp => sp.SocialMediaPlatformPublicationId == socialMediaPlatformPublicationId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<WeConnectPublication> IsNotDeleted(this IQueryable<WeConnectPublication> query)
        {
            query = query.Where(sp => !sp.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>WeConnectPublicationRepository</summary>
    public class WeConnectPublicationRepository : Repository<PlataformaRio2CContext, WeConnectPublication>, IWeConnectPublicationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="WeConnectPublicationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public WeConnectPublicationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<WeConnectPublication> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds the by social media platform publication identifier asynchronous.
        /// </summary>
        /// <param name="socialMediaPlatformPublicationId">The social media platform publication identifier.</param>
        /// <returns></returns>
        public async Task<WeConnectPublication> FindBySocialMediaPlatformPublicationIdAsync(string socialMediaPlatformPublicationId)
        {
            return await this.GetBaseQuery()
                                .FindBySocialMediaPlatformPublicationId(socialMediaPlatformPublicationId)
                                .FirstOrDefaultAsync();
        }

        ///// <summary>Finds the dto by name asynchronous.</summary>
        ///// <param name="name">The name.</param>
        ///// <returns></returns>
        //public async Task<WeConnectPublicationDto> FindDtoByNameAsync(string name)
        //{
        //    return await this.GetBaseQuery()
        //                        .FindByName(name)
        //                        .Select(smp => new WeConnectPublicationDto
        //                        {
        //                            Uid = smp.Uid,
        //                            Name = smp.Name,
        //                            ApiKey = smp.ApiKey,
        //                            EndpointUrl = smp.EndpointUrl,
        //                            IsSyncActive = smp.IsSyncActive
        //                        })
        //                        .FirstOrDefaultAsync();
        //}
    }
}