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
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region AttendeeMusicBand IQueryable Extensions

    /// <summary>
    /// AttendeeMusicBandEvaluationIQueryableExtensions
    /// </summary>
    internal static class AttendeeMusicBandEvaluationIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> IsNotDeleted(this IQueryable<AttendeeMusicBandEvaluation> query)
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
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByEditionId(this IQueryable<AttendeeMusicBandEvaluation> query, int editionId)
        {
            query = query.Where(amb => amb.AttendeeMusicBand.EditionId == editionId && !amb.AttendeeMusicBand.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds by edition, document and string asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorId">The collaborator id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByCollaboratorId(this IQueryable<AttendeeMusicBandEvaluation> query, int collaboratorId)
        {
            query = query.Where(ambe => ambe.EvaluatorUserId == collaboratorId && !ambe.IsDeleted);
            return query;
        }

        /// <summary>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> ExceptMusicBandId(this IQueryable<AttendeeMusicBandEvaluation> query, int? musicBandId)
        {
            if (musicBandId.HasValue)
            {
                query = query.Where(ambe => ambe.AttendeeMusicBand.MusicBandId != musicBandId && !ambe.IsDeleted);
            }
            return query;
        }
    }

    #endregion

    /// <summary>AttendeeMusicBandEvaluationRepository</summary>
    public class AttendeeMusicBandEvaluationRepository : Repository<Context.PlataformaRio2CContext, AttendeeMusicBandEvaluation>, IAttendeeMusicBandEvaluationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandEvaluationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeMusicBandEvaluationRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeMusicBandEvaluation> GetBaseQuery(bool @readonly = false)
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
        public async Task<List<AttendeeMusicBandEvaluation>> FindAllByEditionIdAsync(int editionId)
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
        /// <param name="collaboratorId">The collaborator id.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        public async Task<int> CountByCollaboratorIdAsync(int editionId, int collaboratorId, int? musicBandId)
        {
            var query = this.GetBaseQuery()
                .FindByEditionId(editionId)
                .FindByCollaboratorId(collaboratorId)
                .ExceptMusicBandId(musicBandId);            
            return await query.CountAsync();
        }
    }
}