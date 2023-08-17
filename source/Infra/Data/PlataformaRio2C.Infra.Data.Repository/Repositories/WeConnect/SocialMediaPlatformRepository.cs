// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="SocialMediaPlatformRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Social Media Platform IQueryable Extensions

    /// <summary>
    /// SocialMediaPlatformIQueryableExtensions
    /// </summary>
    internal static class SocialMediaPlatformIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="socialMediaPlatformUid">The social media platform uid.</param>
        /// <returns></returns>
        internal static IQueryable<SocialMediaPlatform> FindByUid(this IQueryable<SocialMediaPlatform> query, Guid socialMediaPlatformUid)
        {
            query = query.Where(sp => sp.Uid == socialMediaPlatformUid);

            return query;
        }

        /// <summary>Finds the name of the by.</summary>
        /// <param name="query">The query.</param>
        /// <param name="socialMediaPlatformName">Name of the social media platform.</param>
        /// <returns></returns>
        internal static IQueryable<SocialMediaPlatform> FindByName(this IQueryable<SocialMediaPlatform> query, string socialMediaPlatformName)
        {
            query = query.Where(sp => sp.Name == socialMediaPlatformName);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<SocialMediaPlatform> IsNotDeleted(this IQueryable<SocialMediaPlatform> query)
        {
            query = query.Where(sp => !sp.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>SocialMediaPlatformRepository</summary>
    public class SocialMediaPlatformRepository : Repository<PlataformaRio2CContext, SocialMediaPlatform>, ISocialMediaPlatformRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SocialMediaPlatformRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public SocialMediaPlatformRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<SocialMediaPlatform> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<SocialMediaPlatform> FindByNameAsync(string name)
        {
            return await this.GetBaseQuery()
                                .FindByName(name)
                                .FirstOrDefaultAsync();
        }

        /// <summary>Finds the dto by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<SocialMediaPlatformDto> FindDtoByNameAsync(string name)
        {
            return await this.GetBaseQuery()
                                .FindByName(name)
                                .Select(smp => new SocialMediaPlatformDto
                                {
                                    Uid = smp.Uid,
                                    Name = smp.Name,
                                    ApiKey = smp.ApiKey,
                                    EndpointUrl = smp.EndpointUrl,
                                    IsSyncActive = smp.IsSyncActive
                                })
                                .FirstOrDefaultAsync();
        }
    }
}