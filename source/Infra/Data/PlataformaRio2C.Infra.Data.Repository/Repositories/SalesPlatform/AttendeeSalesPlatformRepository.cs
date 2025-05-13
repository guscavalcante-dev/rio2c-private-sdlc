// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Attendee Sales Platform IQueryable Extensions

    /// <summary>
    /// AttendeeSalesPlatformIQueryableExtensions
    /// </summary>
    internal static class AttendeeSalesPlatformIQueryableExtensions
    {
        /// <summary>Determines whether this instance is active.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeSalesPlatform> IsActive(this IQueryable<AttendeeSalesPlatform> query)
        {
            query = query.Where(asp => asp.IsActive);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeSalesPlatform> IsNotDeleted(this IQueryable<AttendeeSalesPlatform> query)
        {
            query = query.Where(asp => !asp.IsDeleted
                                       && !asp.SalesPlatform.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformName">Name of the sales platform.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeSalesPlatform> FindByName(this IQueryable<AttendeeSalesPlatform> query, string salesPlatformName)
        {
            query = query.Where(asp => asp.SalesPlatform.Name == salesPlatformName);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeSalesPlatformRepository</summary>
    public class AttendeeSalesPlatformRepository : Repository<PlataformaRio2CContext, AttendeeSalesPlatform>, IAttendeeSalesPlatformRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeSalesPlatformRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeSalesPlatform> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all dto by is active asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<AttendeeSalesPlatformDto>> FindAllDtoByIsActiveAsync()
        {
            return await this.GetBaseQuery()
                                .IsActive()
                                .Select(asp => new AttendeeSalesPlatformDto
                                {
                                    AttendeeSalesPlatform = asp,
                                    SalesPlatform = asp.SalesPlatform,
                                    Edition = asp.Edition,
                                    AttendeeSalesPlatformTicketTypesDtos = asp.AttendeeSalesPlatformTicketTypes
                                                                                    .Where(asptt => !asptt.IsDeleted)
                                                                                    .Select(asptt => new AttendeeSalesPlatformTicketTypeDto
                                                                                    {
                                                                                        AttendeeSalesPlatformTicketType = asptt,
                                                                                        CollaboratorType = asptt.CollaboratorType,
                                                                                        Role = asptt.CollaboratorType.Role
                                                                                    })
                                })
                                .ToListAsync();
        }

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<AttendeeSalesPlatformDto> FindDtoByNameAsync(string name)
        {
            return await this.GetBaseQuery()
                                .FindByName(name)
                                .Select(asp => new AttendeeSalesPlatformDto
                                {
                                    AttendeeSalesPlatform = asp,
                                    SalesPlatform = asp.SalesPlatform
                                })
                                .FirstOrDefaultAsync();
        }
    }
}