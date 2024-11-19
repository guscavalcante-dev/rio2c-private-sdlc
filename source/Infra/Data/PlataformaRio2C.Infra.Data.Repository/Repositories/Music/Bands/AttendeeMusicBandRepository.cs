// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-19-2024
// ***********************************************************************
// <copyright file="AttendeeMusicBandRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;
using LinqKit;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region AttendeeMusicBand IQueryable Extensions

    /// <summary>
    /// AttendeeMusicBandIQueryableExtensions
    /// </summary>
    internal static class AttendeeMusicBandIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBand> IsNotDeleted(this IQueryable<AttendeeMusicBand> query)
        {
            query = query.Where(amb => !amb.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by edition identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBand> FindByEditionId(this IQueryable<AttendeeMusicBand> query, int editionId)
        {
            query = query.Where(amb => amb.EditionId == editionId);

            return query;
        }

        /// <summary>
        /// Finds by edition, document and string asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="document">The document.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBand> FindByResponsible(this IQueryable<AttendeeMusicBand> query, string document, string email)
        {
            query = query.Where(amb => amb.AttendeeMusicBandCollaborators.Any(ambc => 
                ambc.AttendeeCollaborator.Collaborator.Document == document
                && ambc.AttendeeCollaborator.Collaborator.User.Email == email
            ));
            return query;
        }
    }

    #endregion

    /// <summary>AttendeeMusicBandRepository</summary>
    public class AttendeeMusicBandRepository : Repository<Context.PlataformaRio2CContext, AttendeeMusicBand>, IAttendeeMusicBandRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeMusicBandRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeMusicBand> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<AttendeeMusicBand>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId);

            return await query
                            .ToListAsync();
        }

        /// <summary>
        /// Count by edition, document and string asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public async Task<int> CountByResponsibleAsync(int editionId, string document, string email)
        {
            var query = this.GetBaseQuery()
                .FindByEditionId(editionId)
                .FindByResponsible(document, email);
            
            return await query.CountAsync();
        }
    }
}