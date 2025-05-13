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
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<WeConnectPublicationDto>> ToListPagedAsync(this IQueryable<WeConnectPublicationDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<WeConnectPublicationDto> Order(this IQueryable<WeConnectPublicationDto> query)
        {
            query = query.OrderByDescending(wcpDto => wcpDto.CreateDate);

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

        /// <summary>
        /// Finds all dtos paged asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<WeConnectPublicationDto>> FindAllDtosPagedAsync(int page, int pageSize)
        {
            return await this.GetBaseQuery()
                                .Select(wcp => new WeConnectPublicationDto
                                {
                                    Uid = wcp.Uid,
                                    PublicationText = wcp.PublicationText,
                                    SocialMediaPlatformPublicationId = wcp.SocialMediaPlatformPublicationId,
                                    ImageUploadDate = wcp.ImageUploadDate,
                                    CreateDate = wcp.CreateDate,
                                    IsFixedOnTop = wcp.IsFixedOnTop,
                                    IsVideo = wcp.IsVideo,
                                    SocialMediaPlatformDto = new SocialMediaPlatformDto
                                    {
                                        Name = wcp.SocialMediaPlatform.Name,
                                        PublicationsRootUrl = wcp.SocialMediaPlatform.PublicationsRootUrl
                                    }
                                })
                                .Order()
                                .ToListPagedAsync(page, pageSize);
        }
    }
}