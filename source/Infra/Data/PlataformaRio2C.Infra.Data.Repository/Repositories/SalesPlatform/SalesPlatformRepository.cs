// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SalesPlatformRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Sales Platform IQueryable Extensions

    /// <summary>
    /// SalesPlatformIQueryableExtensions
    /// </summary>
    internal static class SalesPlatformIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformUid">The sales platform uid.</param>
        /// <returns></returns>
        internal static IQueryable<SalesPlatform> FindByUid(this IQueryable<SalesPlatform> query, Guid salesPlatformUid)
        {
            query = query.Where(sp => sp.Uid == salesPlatformUid);

            return query;
        }

        /// <summary>Finds the name of the by.</summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformName">Name of the sales platform.</param>
        /// <returns></returns>
        internal static IQueryable<SalesPlatform> FindByName(this IQueryable<SalesPlatform> query, string salesPlatformName)
        {
            query = query.Where(sp => sp.Name == salesPlatformName);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<SalesPlatform> IsNotDeleted(this IQueryable<SalesPlatform> query)
        {
            query = query.Where(sp => !sp.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>SalesPlatformRepository</summary>
    public class SalesPlatformRepository : Repository<PlataformaRio2CContext, SalesPlatform>, ISalesPlatformRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public SalesPlatformRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<SalesPlatform> GetBaseQuery(bool @readonly = false)
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
        public async Task<SalesPlatform> FindByNameAsync(string name)
        {
            return await this.GetBaseQuery()
                                .FindByName(name)
                                .FirstOrDefaultAsync();
        }

        /// <summary>Finds the dto by name asynchronous.</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<SalesPlatformDto> FindDtoByNameAsync(string name)
        {
            return await this.GetBaseQuery()
                                .FindByName(name)
                                .Select(sp => new SalesPlatformDto
                                {
                                    Uid = sp.Uid,
                                    Name = sp.Name,
                                    WebhookSecurityKey = sp.WebhookSecurityKey,
                                    ApiKey = sp.ApiKey,
                                    ApiSecret = sp.ApiSecret,
                                    MaxProcessingCount = sp.MaxProcessingCount,
                                    CreationDate = sp.CreateDate,
                                    UpdateUserId = sp.UpdateUserId,
                                    UpdateDate = sp.UpdateDate,
                                    SecurityStamp = sp.SecurityStamp,
                                    AttendeeSalesPlatforms = sp.AttendeeSalesPlatforms
                                })
                                .FirstOrDefaultAsync();
        }
    }
}