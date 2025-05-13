// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 21-03-2025
// ***********************************************************************
// <copyright file="AttendeeMusicBandRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
        /// Finds the by evaluator user identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="evaluatorUserId">The evaluator user identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByEvaluatorUserId(this IQueryable<AttendeeMusicBandEvaluation> query, int? evaluatorUserId)
        {
            if (evaluatorUserId.HasValue)
            {
                query = query.Where(ambe => ambe.EvaluatorUserId == evaluatorUserId && !ambe.IsDeleted);
            }

            return query;
        }

        /// <summary>
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandId">The music band id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByMusicBandId(this IQueryable<AttendeeMusicBandEvaluation> query, int? musicBandId)
        {
            if (musicBandId.HasValue)
            {
                query = query.Where(ambe => ambe.AttendeeMusicBand.MusicBandId == musicBandId && !ambe.IsDeleted);
            }

            return query;
        }

        /// <summary>
        /// Finds by popular evaluation status id asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="popularEvaluationStatusId">The popular evaluation status id.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> FindByPopularEvaluationStatusId(this IQueryable<AttendeeMusicBandEvaluation> query, int? popularEvaluationStatusId)
        {
            if (popularEvaluationStatusId.HasValue)
            {
                query = query.Where(ambe =>
                    ambe.PopularEvaluationStatusId == popularEvaluationStatusId.Value
                    && !ambe.IsDeleted
                );
            }
            return query;
        }

        /// <summary>
        /// Determines whether [is accepted by collaborator type identifier] [the specified collaborator type identifier].
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeId">The collaborator type identifier.</param>
        /// <param name="editionDto">The edition dto.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeMusicBandEvaluation> IsAcceptedByCollaboratorTypeId(this IQueryable<AttendeeMusicBandEvaluation> query, int collaboratorTypeId, EditionDto editionDto)
        {
            if (collaboratorTypeId == CollaboratorType.ComissionMusic.Id)
            {
                // Projects evaluated by Commission
                query = query.Where(ambe => ambe.CommissionEvaluationStatusId == ProjectEvaluationStatus.Accepted.Id);
            }
            else if (collaboratorTypeId == CollaboratorType.ComissionMusicCurator.Id)
            {
                if (editionDto.IsMusicPitchingRepechageEvaluationOpen())
                {
                    // Projects evaluated by Repechage
                    query = query.Where(ambe => ambe.RepechageEvaluationStatusId == ProjectEvaluationStatus.Accepted.Id);
                }
                else
                {
                    // Projects evaluated by Curator
                    query = query.Where(ambe => ambe.CuratorEvaluationStatusId == ProjectEvaluationStatus.Accepted.Id);
                }
            }

            return query;
        }
    }

    #endregion

    /// <summary>
    /// AttendeeMusicBandEvaluationRepository
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Infra.Data.Repository.Repository&lt;PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext, PlataformaRio2C.Domain.Entities.AttendeeMusicBandEvaluation&gt;" />
    /// <seealso cref="PlataformaRio2C.Domain.Interfaces.IAttendeeMusicBandEvaluationRepository" />
    public class AttendeeMusicBandEvaluationRepository : Repository<Context.PlataformaRio2CContext, AttendeeMusicBandEvaluation>, IAttendeeMusicBandEvaluationRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeMusicBandEvaluationRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="editioRepository">The editio repository.</param>
        public AttendeeMusicBandEvaluationRepository(
            Context.PlataformaRio2CContext context,
            IEditionRepository editioRepository)
            : base(context)
        {
            this.editioRepo = editioRepository;
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
        /// Counts the accepted by collaborator type identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collaboratorTypeId">The collaborator type identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAcceptedByCollaboratorTypeIdAsync(int editionId, int userId, int collaboratorTypeId)
        {
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            var query = this.GetBaseQuery()
                .FindByEditionId(editionId)
                .FindByEvaluatorUserId(userId)
                .IsAcceptedByCollaboratorTypeId(collaboratorTypeId, editionDto);

            return await query.CountAsync();
        }

        /// <summary>
        /// Counts the by popular evaluation asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="musicBandId">The music band identifier.</param>
        /// <param name="popularEvaluationStatusId">The popular evaluation status identifier.</param>
        /// <returns></returns>
        public async Task<int> CountByPopularEvaluationAsync(int editionId, int? musicBandId, int? popularEvaluationStatusId)
        {
            var query = this.GetBaseQuery()
                .FindByMusicBandId(musicBandId)
                .FindByEditionId(editionId)
                .FindByPopularEvaluationStatusId(popularEvaluationStatusId);

            return await query.CountAsync();
        }
    }
}
