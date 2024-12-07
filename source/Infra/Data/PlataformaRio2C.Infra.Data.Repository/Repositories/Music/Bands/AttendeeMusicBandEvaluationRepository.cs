// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-02-2024
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
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByCollaboratorId(this IQueryable<AttendeeMusicBandEvaluation> query, List<int?> collaboratorId)
        {
            if (collaboratorId != null && collaboratorId.Count > 0)
            {
                query = query.Where(ambe =>
                    collaboratorId.Contains(ambe.EvaluatorUserId)
                    && !ambe.IsDeleted
                );
            }
            return query;
        }

        /// <summary>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByMusicBandId(this IQueryable<AttendeeMusicBandEvaluation> query, List<int?> musicBandId)
        {
            if (musicBandId != null && musicBandId.Count > 0)
            {
                query = query.Where(ambe =>
                    musicBandId.Contains(ambe.AttendeeMusicBand.MusicBandId)
                    && !ambe.IsDeleted
                );
            }
            return query;
        }

        /// <summary>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> ExceptMusicBandId(this IQueryable<AttendeeMusicBandEvaluation> query, List<int?> musicBandId)
        {
            if (musicBandId != null && musicBandId.Count > 0)
            {
                query = query.Where(ambe =>
                    !musicBandId.Contains(ambe.AttendeeMusicBand.MusicBandId)
                    && !ambe.IsDeleted
                );
            }
            return query;
        }

        /// <summary>
        /// Finds by commission evaluation status id asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="commissionEvaluationStatusId">The commission evaluation status id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByCommissionEvaluationStatusId(this IQueryable<AttendeeMusicBandEvaluation> query, List<int?> commissionEvaluationStatusId)
        {
            if (commissionEvaluationStatusId != null && commissionEvaluationStatusId.Count > 0)
            {
                query = query.Where(ambe =>
                    commissionEvaluationStatusId.Contains(ambe.CommissionEvaluationStatusId)
                    && !ambe.IsDeleted
                );
            }
            return query;
        }

        /// <summary>
        /// Finds by curator evaluation status id asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="curatorEvaluationStatusId">The curator evaluation status id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByCuratorEvaluationStatusId(this IQueryable<AttendeeMusicBandEvaluation> query, List<int?> curatorEvaluationStatusId)
        {
            if (curatorEvaluationStatusId != null && curatorEvaluationStatusId.Count > 0)
            {
                query = query.Where(ambe =>
                    curatorEvaluationStatusId.Contains(ambe.CuratorEvaluationStatusId)
                    && !ambe.IsDeleted
                );
            }
            return query;
        }

        /// <summary>
        /// Finds by repechage evaluation status id asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="popularEvaluationStatusId">The popular evaluation status id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByPopularEvaluationStatusId(this IQueryable<AttendeeMusicBandEvaluation> query, List<int?> popularEvaluationStatusId)
        {
            if (popularEvaluationStatusId != null && popularEvaluationStatusId.Count > 0)
            {
                query = query.Where(ambe =>
                    popularEvaluationStatusId.Contains(ambe.PopularEvaluationStatusId)
                    && !ambe.IsDeleted
                );
            }
            return query;
        }

        /// <summary>
        /// Finds by repechage evaluation status id asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="repechageEvaluationStatusId">The repechage evaluation status id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByRepechageEvaluationStatusId(this IQueryable<AttendeeMusicBandEvaluation> query, List<int?> repechageEvaluationStatusId)
        {
            if (repechageEvaluationStatusId != null && repechageEvaluationStatusId.Count > 0)
            {
                query = query.Where(ambe =>
                    repechageEvaluationStatusId.Contains(ambe.RepechageEvaluationStatusId)
                    && !ambe.IsDeleted
                );
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
        /// Count by edition, music band id and collaborator id asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorId">The collaborator id.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        public async Task<int> CountByCommissionMemberAsync(int editionId, List<int?> musicBandId, List<int?> collaboratorId)
        {
            var query = this.GetBaseQuery()
                .ExceptMusicBandId(musicBandId)
                .FindByEditionId(editionId)
                .FindByCollaboratorId(collaboratorId)
                .FindByCommissionEvaluationStatusId(new List<int?>() {
                    ProjectEvaluationStatus.Accepted.Id,
                    ProjectEvaluationStatus.Refused.Id
                });
            return await query.CountAsync();
        }

        /// <summary>
        /// Count by edition, music band id asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        public async Task<int> CountByCuratorAsync(int editionId, List<int?> musicBandId)
        {
            var query = this.GetBaseQuery()
                .ExceptMusicBandId(musicBandId)
                .FindByEditionId(editionId)
                .FindByCuratorEvaluationStatusId(new List<int?>() {
                    ProjectEvaluationStatus.Accepted.Id,
                    ProjectEvaluationStatus.Refused.Id
                });
            return await query.CountAsync();
        }

        /// <summary>
        /// Count by edition, music band id asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        public async Task<int> CountByRepechageAsync(int editionId, List<int?> musicBandId)
        {
            var query = this.GetBaseQuery()
                .ExceptMusicBandId(musicBandId)
                .FindByEditionId(editionId)
                .FindByRepechageEvaluationStatusId(new List<int?>() {
                    ProjectEvaluationStatus.Accepted.Id,
                    ProjectEvaluationStatus.Refused.Id
                });
            return await query.CountAsync();
        }

        /// <summary>
        /// Count by edition, music band id asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <param name="popularEvaluationStatusId">The popular evaluation status id.</param>
        /// <returns></returns>
        public async Task<int> CountByPopularEvaluationAsync(int editionId, List<int?> musicBandId, List<int?> popularEvaluationStatusId)
        {
            var query = this.GetBaseQuery()
                .FindByMusicBandId(musicBandId)
                .FindByEditionId(editionId)
                .FindByPopularEvaluationStatusId(popularEvaluationStatusId);
            return await query.CountAsync();
        }
    }
}