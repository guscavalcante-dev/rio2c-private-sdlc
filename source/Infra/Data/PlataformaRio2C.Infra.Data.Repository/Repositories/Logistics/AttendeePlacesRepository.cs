// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
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
    #region IQueryable Extensions

    /// <summary>
    /// CollaboratorIQueryableExtensions
    /// </summary>
    internal static class AttendeePlaceIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> IsNotDeleted(this IQueryable<AttendeePlace> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> FindByUid(this IQueryable<AttendeePlace> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeePlace> FindByEditionId(this IQueryable<AttendeePlace> query, int editionId)
        {
            return query.Where(e => e.EditionId == editionId);
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

        /// <summary>Finds all dtos by edition.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public Task<List<AttendeePlaceDto>> FindAllDtosByEdition(int editionId)
        {
            return this.GetBaseQuery()
                            .FindByEditionId(editionId)
                            .Select(e => new AttendeePlaceDto()
                            {
                                Id = e.Id,
                                Name = e.Place.Name
                            })
                            .OrderBy(e => e.Name)
                            .ToListAsync();
        }
    }
}
