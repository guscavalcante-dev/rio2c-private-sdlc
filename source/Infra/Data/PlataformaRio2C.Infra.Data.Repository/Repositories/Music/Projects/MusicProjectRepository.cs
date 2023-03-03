// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-03-2023
// ***********************************************************************
// <copyright file="MusicProjectRepository.cs" company="Softo">
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

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region MusicProject IQueryable Extensions

    /// <summary>
    /// MusicProjectIQueryableExtensions
    /// </summary>
    internal static class MusicProjectIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByUid(this IQueryable<MusicProject> query, Guid musicProjectUid)
        {
            query = query.Where(mp => mp.Uid == musicProjectUid);

            return query;
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="musicProjectId">The music project identifier.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindById(this IQueryable<MusicProject> query, int musicProjectId)
        {
            query = query.Where(mp => mp.Id == musicProjectId);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByEditionId(this IQueryable<MusicProject> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(mp => (showAllEditions || mp.AttendeeMusicBand.EditionId == editionId));

            return query;
        }

        /// <summary>
        /// Finds the by is evaluated.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByIsEvaluated(this IQueryable<MusicProject> query)
        {
            query = query.Where(mp => mp.AttendeeMusicBand.Grade != null);

            return query;
        }

        /// <summary>Finds the by music genre uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByMusicGenreUid(this IQueryable<MusicProject> query, Guid? musicGenreUid)
        {
            if (musicGenreUid.HasValue)
            {
                query = query.Where(mp => mp.AttendeeMusicBand.MusicBand.MusicBandGenres.Any(mbg => !mbg.IsDeleted
                                                                                                    && !mbg.MusicGenre.IsDeleted
                                                                                                    && mbg.MusicGenre.Uid == musicGenreUid));
            }

            return query;
        }

        /// <summary>Finds the by attendee music band uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeMusicBandUid">The attendee music band uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByAttendeeMusicBandUid(this IQueryable<MusicProject> query, Guid attendeeMusicBandUid)
        {
            query = query.Where(mp => mp.AttendeeMusicBand.Uid == attendeeMusicBandUid);

            return query;
        }

        /// <summary>Finds the by attendee music bands uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeMusicBandsUids">The attendee music bands uids.</param>
        /// <param name="showAll">if set to <c>true</c> [show all].</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByAttendeeMusicBandsUids(this IQueryable<MusicProject> query, List<Guid> attendeeMusicBandsUids, bool showAll)
        {
            if (!showAll && attendeeMusicBandsUids?.Any() == true)
            {
                query = query.Where(mp => attendeeMusicBandsUids.Contains(mp.AttendeeMusicBand.Uid));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByKeywords(this IQueryable<MusicProject> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<MusicProject>(false);
                var innerMusicBandNameWhere = PredicateBuilder.New<MusicProject>(true);
                var innerMusicBandTypeNameWhere = PredicateBuilder.New<MusicProject>(true);
                var innerMusicGenreNameWhere = PredicateBuilder.New<MusicProject>(true);
                var innerTargetAudienceNameWhere = PredicateBuilder.New<MusicProject>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerMusicBandNameWhere = innerMusicBandNameWhere.Or(mp => mp.AttendeeMusicBand.MusicBand.Name.Contains(keyword));
                        innerMusicBandTypeNameWhere = innerMusicBandTypeNameWhere.Or(mp => mp.AttendeeMusicBand.MusicBand.MusicBandType.Name.Contains(keyword));
                        innerMusicGenreNameWhere = innerMusicGenreNameWhere.Or(mp => mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                                                    .Any(mbg => mbg.MusicGenre.Name.Contains(keyword)
                                                                                                                && !mbg.IsDeleted
                                                                                                                && !mbg.MusicGenre.IsDeleted));
                        innerTargetAudienceNameWhere = innerTargetAudienceNameWhere.Or(mp => mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                                                    .Any(mta => mta.TargetAudience.Name.Contains(keyword)
                                                                                                                && !mta.IsDeleted
                                                                                                                && !mta.TargetAudience.IsDeleted));
                    }
                }

                outerWhere = outerWhere.Or(innerMusicBandNameWhere);
                outerWhere = outerWhere.Or(innerMusicBandTypeNameWhere);
                outerWhere = outerWhere.Or(innerMusicGenreNameWhere);
                outerWhere = outerWhere.Or(innerTargetAudienceNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Shows the business rounds.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> ShowBusinessRounds(this IQueryable<MusicProject> query, bool showBusinessRounds = false)
        {
            if (showBusinessRounds)
            {
                query = query.Where(mp => mp.AttendeeMusicBand.WouldYouLikeParticipateBusinessRound == true);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> IsNotDeleted(this IQueryable<MusicProject> query)
        {
            query = query.Where(mp => !mp.IsDeleted
                                      && !mp.AttendeeMusicBand.IsDeleted
                                      && !mp.AttendeeMusicBand.MusicBand.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> Order(this IQueryable<MusicProject> query)
        {
            query = query.OrderBy(mp => mp.CreateDate);

            return query;
        }

        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicProject>> ToListPagedAsync(this IQueryable<MusicProject> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region MusicProjectDto IQueryable Extensions

    /// <summary>
    /// MusicProjectDtoIQueryableExtensions
    /// </summary>
    internal static class MusicProjectDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicProjectDto>> ToListPagedAsync(this IQueryable<MusicProjectDto> query, int page, int pageSize)
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
        internal static IQueryable<MusicProjectDto> Order(this IQueryable<MusicProjectDto> query)
        {
            query = query.OrderBy(mpd => mpd.MusicProject.CreateDate);

            return query;
        }
    }

    #endregion

    #region MusicProjectJsonDto IQueryable Extensions

    /// <summary>MusicProjectJsonDtoIQueryableExtensions</summary>
    internal static class MusicProjectJsonDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicProjectJsonDto>> ToListPagedAsync(this IQueryable<MusicProjectJsonDto> query, int page, int pageSize)
        {
            //// Page the list
            //page++;

            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion 

    /// <summary>MusicProjectRepository</summary>
    public class MusicProjectRepository : Repository<Context.PlataformaRio2CContext, MusicProject>, IMusicProjectRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>Initializes a new instance of the <see cref="MusicProjectRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicProjectRepository(
            Context.PlataformaRio2CContext context,
            IEditionRepository editionRepository
            )
            : base(context)
        {
            this.editioRepo = editionRepository;
        }

        #region Private Methods

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicProject> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all music projects asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <returns></returns>
        private async Task<List<MusicProject>> FindAllMusicProjectsAsync(int editionId, string searchKeywords, Guid? musicGenreUid, bool showBusinessRounds)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(searchKeywords)
                                .FindByMusicGenreUid(musicGenreUid)
                                .ShowBusinessRounds(showBusinessRounds);

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all music project dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        private async Task<List<MusicProjectDto>> FindAllMusicProjectDtosAsync(int editionId, string searchKeywords, Guid? musicGenreUid)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId)
                               .FindByKeywords(searchKeywords)
                               .FindByMusicGenreUid(musicGenreUid)
                               .Select(mp => new MusicProjectDto
                               {
                                   MusicProject = mp,
                                   AttendeeMusicBandDto = new MusicBandDto
                                   {
                                       AttendeeMusicBand = mp.AttendeeMusicBand,
                                       MusicBand = mp.AttendeeMusicBand.MusicBand,
                                       MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                       MusicBandGenreDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                   .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                   .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                   .Select(mbg => new MusicBandGenreDto
                                                                   {
                                                                       MusicBandGenre = mbg,
                                                                       MusicGenre = mbg.MusicGenre
                                                                   }),
                                       MusicBandTargetAudienceDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                           .Where(mbta => !mbta.IsDeleted && !mbta.TargetAudience.IsDeleted)
                                                                           .OrderBy(mbta => mbta.TargetAudience.DisplayOrder)
                                                                           .Select(mbta => new MusicBandTargetAudienceDto
                                                                           {
                                                                               MusicBandTargetAudience = mbta,
                                                                               TargetAudience = mbta.TargetAudience
                                                                           }),
                                       AttendeeMusicBandEvaluationsDtos = mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                                                   .Where(ambe => !ambe.IsDeleted)
                                                                                   .Select(ambe => new AttendeeMusicBandEvaluationDto
                                                                                   {
                                                                                       AttendeeMusicBandEvaluation = ambe,
                                                                                       EvaluatorUser = ambe.EvaluatorUser
                                                                                   }).ToList()
                                   }
                               });

            return await query
                            .Order()
                            .ToListAsync();
            //.ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all json dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        private async Task<List<MusicProjectJsonDto>> FindAllJsonDtosAsync(int editionId, string searchKeywords, Guid? musicGenreUid, bool showBusinessRounds, List<Tuple<string, string>> sortColumns)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId, false)
                               .FindByKeywords(searchKeywords)
                               .FindByMusicGenreUid(musicGenreUid)
                               .ShowBusinessRounds(showBusinessRounds)
                               .DynamicOrder<MusicProject>(
                                   sortColumns,
                                   null,
                                   new List<string> { "CreateDate", "UpdateDate" }, "CreateDate")
                               .Select(mp => new MusicProjectJsonDto
                               {
                                   MusicProjectId = mp.Id,
                                   MusicProjectUid = mp.Uid,
                                   AttendeeMusicBandId = mp.AttendeeMusicBand.Id,
                                   MusicBandUid = mp.AttendeeMusicBand.MusicBand.Uid,
                                   MusicBandName = mp.AttendeeMusicBand.MusicBand.Name,
                                   MusicBandTypeName = mp.AttendeeMusicBand.MusicBand.MusicBandType.Name,
                                   ImageUploadDate = mp.AttendeeMusicBand.MusicBand.ImageUploadDate,
                                   Grade = mp.AttendeeMusicBand.Grade,
                                   EvaluationsCount = mp.AttendeeMusicBand.AttendeeMusicBandEvaluations.Count(ambe => !ambe.IsDeleted),
                                   MusicGenreNames = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                           .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                           .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                           .Select(mbg => mbg.MusicGenre.Name).ToList(),
                                   MusicTargetAudiencesNames = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                       .Where(mbg => !mbg.IsDeleted && !mbg.TargetAudience.IsDeleted)
                                                                       .OrderBy(mbg => mbg.TargetAudience.DisplayOrder)
                                                                       .Select(mbg => mbg.TargetAudience.Name).ToList(),
                                   CreateDate = mp.CreateDate,
                                   UpdateDate = mp.UpdateDate,
                                   WouldYouLikeParticipateBusinessRound = mp.AttendeeMusicBand.WouldYouLikeParticipateBusinessRound
                               });

            return await query
                            .ToListAsync();
        }

        #endregion

        /// <summary>Finds all dtos to evaluate asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicProjectDto>> FindAllDtosPagedAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, int page, int pageSize)
        {
            var musicProjectsDtos = await this.FindAllMusicProjectDtosAsync(editionId, searchKeywords, musicGenreUid);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedMusicBandsIds = await this.FindAllApprovedAttendeeMusicBandsIdsAsync(editionId);

            IEnumerable<MusicProjectDto> musicProjectDtosResult = musicProjectsDtos;
            if (editionDto.IsMusicProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectDtosResult = new List<MusicProjectDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectDtosResult = new List<MusicProjectDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectDtosResult = musicProjectsDtos.Where(w => approvedMusicBandsIds.Contains(w.AttendeeMusicBandDto.AttendeeMusicBand.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectDtosResult = musicProjectsDtos.Where(w => !approvedMusicBandsIds.Contains(w.AttendeeMusicBandDto.AttendeeMusicBand.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    musicProjectDtosResult = new List<MusicProjectDto>();
                }

                #endregion
            }

            return await musicProjectDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all json dtos paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicProjectJsonDto>> FindAllJsonDtosPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            Guid? evaluationStatusUid,
            bool showBusinessRounds,
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns)
        {
            var musicProjectJsonDtos = await this.FindAllJsonDtosAsync(editionId, searchKeywords, musicGenreUid, showBusinessRounds, sortColumns);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedMusicBandsIds = await this.FindAllApprovedAttendeeMusicBandsIdsAsync(editionId);

            IEnumerable<MusicProjectJsonDto> musicProjectJsonDtosResult = musicProjectJsonDtos;
            if (editionDto.IsMusicProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectJsonDtosResult = new List<MusicProjectJsonDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectJsonDtosResult = new List<MusicProjectJsonDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectJsonDtosResult = musicProjectJsonDtos.Where(w => approvedMusicBandsIds.Contains(w.AttendeeMusicBandId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectJsonDtosResult = musicProjectJsonDtos.Where(w => !approvedMusicBandsIds.Contains(w.AttendeeMusicBandId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    musicProjectJsonDtosResult = new List<MusicProjectJsonDto>();
                }

                #endregion
            }

            return await musicProjectJsonDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>Finds the dto to evaluate asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindDtoToEvaluateAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                        MusicBandGenreDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                    .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                    .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                    .Select(mbg => new MusicBandGenreDto
                                                                    {
                                                                        MusicBandGenre = mbg,
                                                                        MusicGenre = mbg.MusicGenre
                                                                    }),
                                        MusicBandTargetAudienceDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                            .Where(mbta => !mbta.IsDeleted && !mbta.TargetAudience.IsDeleted)
                                                                            .OrderBy(mbta => mbta.TargetAudience.DisplayOrder)
                                                                            .Select(mbta => new MusicBandTargetAudienceDto
                                                                            {
                                                                                MusicBandTargetAudience = mbta,
                                                                                TargetAudience = mbta.TargetAudience
                                                                            })
                                    },
                                    //MusicProjectEvaluationDto = new MusicProjectEvaluationDto
                                    //{
                                    //    EvaluationCollaboratorUser = mp.EvaluationUser,
                                    //    EvaluationCollaborator = mp.EvaluationUser.Collaborator,
                                    //    ProjectEvaluationStatus = mp.ProjectEvaluationStatus,
                                    //    ProjectEvaluationRefuseReason = mp.ProjectEvaluationRefuseReason,
                                    //    Reason = mp.Reason,
                                    //    EvaluationDate = mp.EvaluationDate
                                    //}
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="musicProjectId">The music project identifier.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindDtoToEvaluateAsync(int musicProjectId)
        {
            var query = this.GetBaseQuery()
                                .FindById(musicProjectId)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                        MusicBandGenreDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                    .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                    .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                    .Select(mbg => new MusicBandGenreDto
                                                                    {
                                                                        MusicBandGenre = mbg,
                                                                        MusicGenre = mbg.MusicGenre
                                                                    }),
                                        MusicBandTargetAudienceDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                            .Where(mbta => !mbta.IsDeleted && !mbta.TargetAudience.IsDeleted)
                                                                            .OrderBy(mbta => mbta.TargetAudience.DisplayOrder)
                                                                            .Select(mbta => new MusicBandTargetAudienceDto
                                                                            {
                                                                                MusicBandTargetAudience = mbta,
                                                                                TargetAudience = mbta.TargetAudience
                                                                            })
                                    },
                                    //MusicProjectEvaluationDto = new MusicProjectEvaluationDto
                                    //{
                                    //    EvaluationCollaboratorUser = mp.EvaluationUser,
                                    //    EvaluationCollaborator = mp.EvaluationUser.Collaborator,
                                    //    ProjectEvaluationStatus = mp.ProjectEvaluationStatus,
                                    //    ProjectEvaluationRefuseReason = mp.ProjectEvaluationRefuseReason,
                                    //    Reason = mp.Reason,
                                    //    EvaluationDate = mp.EvaluationDate
                                    //}
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindMainInformationWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                        MusicBandGenreDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                    .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                    .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                    .Select(mbg => new MusicBandGenreDto
                                                                    {
                                                                        MusicBandGenre = mbg,
                                                                        MusicGenre = mbg.MusicGenre
                                                                    }),
                                        MusicBandTargetAudienceDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                            .Where(mbta => !mbta.IsDeleted && !mbta.TargetAudience.IsDeleted)
                                                                            .OrderBy(mbta => mbta.TargetAudience.DisplayOrder)
                                                                            .Select(mbta => new MusicBandTargetAudienceDto
                                                                            {
                                                                                MusicBandTargetAudience = mbta,
                                                                                TargetAudience = mbta.TargetAudience
                                                                            }),
                                        AttendeeMusicBandEvaluationsDtos = mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                                                    .Where(ambe => !ambe.IsDeleted)
                                                                                    .Select(ambe => new AttendeeMusicBandEvaluationDto
                                                                                    {
                                                                                        AttendeeMusicBandEvaluation = ambe,
                                                                                        EvaluatorUser = ambe.EvaluatorUser
                                                                                    }).ToList()
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the members widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindMembersWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandMembers = mp.AttendeeMusicBand.MusicBand.MusicBandMembers.Where(mbm => !mbm.IsDeleted)
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the team members widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindTeamMembersWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        MusicBandTeamMembers = mp.AttendeeMusicBand.MusicBand.MusicBandTeamMembers.Where(mbm => !mbm.IsDeleted)
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the released music projects widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindReleasedProjectsWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        ReleasedMusicProjects = mp.AttendeeMusicBand.MusicBand.ReleasedMusicProjects.Where(rmp => !rmp.IsDeleted)
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the project responsible widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindProjectResponsibleWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        AttendeeMusicBandCollaboratorDto = mp.AttendeeMusicBand.AttendeeMusicBandCollaborators
                                                                                .Where(amb => !amb.IsDeleted && !amb.AttendeeCollaborator.IsDeleted && !amb.AttendeeCollaborator.Collaborator.IsDeleted)
                                                                                .Select(ambc => new AttendeeMusicBandCollaboratorDto
                                                                                {
                                                                                    AttendeeMusicBandCollaborator = ambc,
                                                                                    AttendeeCollaborator = ambc.AttendeeCollaborator,
                                                                                    Collaborator = ambc.AttendeeCollaborator.Collaborator,
                                                                                    User = ambc.AttendeeCollaborator.Collaborator.User,
                                                                                    AddressDto = new AddressDto 
                                                                                    {
                                                                                        Address = ambc.AttendeeCollaborator.Collaborator.Address,
                                                                                        City = ambc.AttendeeCollaborator.Collaborator.Address.City,
                                                                                        State = ambc.AttendeeCollaborator.Collaborator.Address.State,
                                                                                        Country = ambc.AttendeeCollaborator.Collaborator.Address.Country
                                                                                    }
                                                                                })
                                                                                .FirstOrDefault()
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the clippings widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindClippingWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the video and music widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindVideoAndMusicWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the social networks widget dto asynchronous.</summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindSocialNetworksWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicProjectUid)
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                    }
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the evaluation grade widget dto asynchronous.
        /// </summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindEvaluationGradeWidgetDtoAsync(Guid musicProjectUid, int userId)
        {
            var query = this.GetBaseQuery()
                               .FindByUid(musicProjectUid)
                               .Select(mp => new MusicProjectDto
                               {
                                   MusicProject = mp,
                                   AttendeeMusicBandDto = new MusicBandDto()
                                   {
                                       AttendeeMusicBand = mp.AttendeeMusicBand,
                                       MusicBand = mp.AttendeeMusicBand.MusicBand,
                                       AttendeeMusicBandEvaluationsDtos = mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                                                    .Where(ambe => !ambe.IsDeleted)
                                                                                    .Select(ambe => new AttendeeMusicBandEvaluationDto
                                                                                    {
                                                                                        AttendeeMusicBandEvaluation = ambe,
                                                                                        EvaluatorUser = ambe.EvaluatorUser
                                                                                    }).ToList(),

                                       //Current AttendeeMusicBandEvaluation by user Id
                                       AttendeeMusicBandEvaluationDto = mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                                                    .Where(ambe => !ambe.IsDeleted && ambe.EvaluatorUserId == userId)
                                                                                    .Select(ambe => new AttendeeMusicBandEvaluationDto
                                                                                    {
                                                                                        AttendeeMusicBandEvaluation = ambe,
                                                                                        EvaluatorUser = ambe.EvaluatorUser
                                                                                    }).FirstOrDefault()
                                   },
                               });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the evaluators widget dto asynchronous.
        /// </summary>
        /// <param name="musicProjectUid">The music project uid.</param>
        /// <returns></returns>
        public async Task<MusicProjectDto> FindEvaluatorsWidgetDtoAsync(Guid musicProjectUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUid(musicProjectUid)
                               .Select(mp => new MusicProjectDto
                               {
                                   MusicProject = mp,
                                   AttendeeMusicBandDto = new MusicBandDto()
                                   {
                                       AttendeeMusicBand = mp.AttendeeMusicBand,
                                       MusicBand = mp.AttendeeMusicBand.MusicBand,
                                       AttendeeMusicBandEvaluationsDtos = mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                                                    .Where(ambe => !ambe.IsDeleted)
                                                                                    .Select(ambe => new AttendeeMusicBandEvaluationDto
                                                                                    {
                                                                                        AttendeeMusicBandEvaluation = ambe,
                                                                                        EvaluatorUser = ambe.EvaluatorUser
                                                                                    }).ToList()
                                   },
                               });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all approved attendee music bands asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<MusicProjectDto>> FindAllApprovedAttendeeMusicBandsAsync(int editionId)
        {
            var edition = await this.editioRepo.FindByIdAsync(editionId);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByIsEvaluated()
                                .Select(mp => new MusicProjectDto
                                {
                                    MusicProject = mp,
                                    AttendeeMusicBandDto = new MusicBandDto()
                                    {
                                        AttendeeMusicBand = mp.AttendeeMusicBand,
                                        MusicBand = mp.AttendeeMusicBand.MusicBand,
                                        AttendeeMusicBandEvaluationsDtos = mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                                                    .Where(ambe => !ambe.IsDeleted)
                                                                                    .Select(ambe => new AttendeeMusicBandEvaluationDto
                                                                                    {
                                                                                        AttendeeMusicBandEvaluation = ambe,
                                                                                        EvaluatorUser = ambe.EvaluatorUser
                                                                                    }).ToList()
                                    },
                                });

            return await query
                            .OrderByDescending(mp => mp.AttendeeMusicBandDto.AttendeeMusicBand.Grade)
                            .Take(edition.MusicCommissionMaximumApprovedBandsCount)
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all approved attendee music bands ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllApprovedAttendeeMusicBandsIdsAsync(int editionId)
        {
            var edition = await this.editioRepo.FindByIdAsync(editionId);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByIsEvaluated();

            return await query
                            .OrderByDescending(mp => mp.AttendeeMusicBand.Grade)
                            .Take(edition.MusicCommissionMaximumApprovedBandsCount)
                            .Select(mp => mp.AttendeeMusicBand.Id)
                            .ToArrayAsync();
        }

        /// <summary>
        /// Finds all music projects ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllMusicProjectsIdsAsync(int editionId)
        {
            var query = this.GetBaseQuery(@readonly: true)
                                .FindByEditionId(editionId)
                                .Select(mp => mp.Id);

            return await query
                            .OrderBy(mpId => mpId)
                            .ToArrayAsync();
        }

        /// <summary>
        /// Finds all music projects ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllMusicProjectsIdsPagedAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, bool showBusinessRounds, int page, int pageSize)
        {
            var musicProjects = await this.FindAllMusicProjectsAsync(editionId, searchKeywords, musicGenreUid, showBusinessRounds);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedMusicBandsIds = await this.FindAllApprovedAttendeeMusicBandsIdsAsync(editionId);

            IEnumerable<MusicProject> musicProjectsResult = musicProjects;
            if (editionDto.IsMusicProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectsResult = new List<MusicProject>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectsResult = new List<MusicProject>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectsResult = musicProjects.Where(mp => approvedMusicBandsIds.Contains(mp.AttendeeMusicBandId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectsResult = musicProjects.Where(mp => !approvedMusicBandsIds.Contains(mp.AttendeeMusicBandId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    musicProjectsResult = new List<MusicProject>();
                }

                #endregion
            }

            var musicProjectsPagedList = await musicProjectsResult
                                                 .ToPagedListAsync(page, pageSize);

            return musicProjectsPagedList
                            .Select(mp => mp.Id)
                            .OrderBy(mpId => mpId)
                            .ToArray();
        }

        /// <summary>Counts the asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountAsync(int editionId, bool showAllEditions = false)

        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions);

            return await query.CountAsync();
        }

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int> CountPagedAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, bool showBusinessRounds, int page, int pageSize)
        {
            var musicProjects = await this.FindAllMusicProjectsAsync(editionId, searchKeywords, musicGenreUid, showBusinessRounds);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedMusicBandsIds = await this.FindAllApprovedAttendeeMusicBandsIdsAsync(editionId);

            IEnumerable<MusicProject> musicProjectsResult = musicProjects;
            if (editionDto.IsMusicProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectsResult = new List<MusicProject>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectsResult = new List<MusicProject>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    musicProjectsResult = musicProjects.Where(mp => approvedMusicBandsIds.Contains(mp.AttendeeMusicBandId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    musicProjectsResult = musicProjects.Where(mp => !approvedMusicBandsIds.Contains(mp.AttendeeMusicBandId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    musicProjectsResult = new List<MusicProject>();
                }

                #endregion
            }

            var musicProjectsPagedList = await musicProjectsResult
                                                 .ToPagedListAsync(page, pageSize);

            return musicProjectsPagedList.Count;
        }

        public async Task<List<MusicBandGroupedByGenreDto>> FindEditionCountPieWidgetDto(int editionId)
        {
            var query = this.GetBaseQuery()
                                .IsNotDeleted()
                                .FindByEditionId(editionId, false);

            return await query.SelectMany(mp => mp.AttendeeMusicBand.MusicBand.MusicBandGenres)
                                    .GroupBy(mbg => mbg.MusicGenre.Name)
                                    .Select(musicBandGenresGroupedByMusicGenreName => new MusicBandGroupedByGenreDto 
                                    { 
                                        MusicGenreName = musicBandGenresGroupedByMusicGenreName.Key,
                                        MusicBandsTotalCount = musicBandGenresGroupedByMusicGenreName.Count()
                                    })
                                    .OrderByDescending(mbg => mbg.MusicBandsTotalCount)
                                    .ToListAsync();
        }
    }
}