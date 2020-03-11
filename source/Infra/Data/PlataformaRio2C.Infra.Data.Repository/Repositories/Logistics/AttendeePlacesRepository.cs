// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="AttendeePlacesRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region AttendeePlace IQueryable Extensions

    /// <summary>
    /// AttendeePlaceIQueryableExtensions
    /// </summary>
    internal static class AttendeePlaceIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> FindByUid(this IQueryable<AttendeePlace> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> FindByEditionId(this IQueryable<AttendeePlace> query, int editionId)
        {
            return query.Where(e => e.EditionId == editionId);
        }

        /// <summary>Determines whether the specified is hotel is hotel.</summary>
        /// <param name="query">The query.</param>
        /// <param name="isHotel">The is hotel.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> IsHotel(this IQueryable<AttendeePlace> query, bool? isHotel)
        {
            if (isHotel.HasValue)
            {
                query = query.Where(ap => ap.Place.IsHotel == isHotel);
            }

            return query;
        }

        /// <summary>Determines whether the specified is airport is airport.</summary>
        /// <param name="query">The query.</param>
        /// <param name="isAirport">The is airport.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> IsAirport(this IQueryable<AttendeePlace> query, bool? isAirport)
        {
            if (isAirport.HasValue)
            {
                query = query.Where(ap => ap.Place.IsAirport == isAirport);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> IsNotDeleted(this IQueryable<AttendeePlace> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeePlacesRepository</summary>
    public class AttendeePlacesRepository : Repository<PlataformaRio2CContext, AttendeePlace>, IAttendeePlacesRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeePlacesRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeePlacesRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeePlace> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all dropdown dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="isHotel">The is hotel.</param>
        /// <param name="isAirport">The is airport.</param>
        /// <returns></returns>
        public Task<List<AttendeePlaceDropdownDto>> FindAllDropdownDtosAsync(int editionId, bool? isHotel = null, bool? isAirport = null)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .IsHotel(isHotel)
                                .IsAirport(isAirport)
                                .Select(e => new AttendeePlaceDropdownDto
                                {
                                    Id = e.Id,
                                    Name = e.Place.Name
                                });

            return query
                    .OrderBy(e => e.Name)
                    .ToListAsync();
        }
    }
}
