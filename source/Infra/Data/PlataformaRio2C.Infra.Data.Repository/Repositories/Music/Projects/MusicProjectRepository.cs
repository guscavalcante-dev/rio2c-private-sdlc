// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-22-2024
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

        /// <summary>
        /// Finds the by evaluation status identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="evaluationStatusId">The evaluation status identifier.</param>
        /// <param name="userAccessControlDto">The user access control dto.</param>
        /// <returns></returns>
        internal static IQueryable<MusicProject> FindByEvaluationStatusId(this IQueryable<MusicProject> query, int? evaluationStatusId, EditionDto editionDto, UserAccessControlDto userAccessControlDto)
        {
            if (userAccessControlDto.IsCommissionMusic())
            {
                #region Projects evaluated by Commission

                // Projects not evaluated by another Commission
                query = query.Where(mp => mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                .All(ambe => ambe.CuratorEvaluationDate == null
                                                && ambe.PopularEvaluationDate == null
                                                && ambe.RepechageEvaluationDate == null));

                if (evaluationStatusId.HasValue)
                {
                    if (evaluationStatusId == ProjectEvaluationStatus.UnderEvaluation.Id)
                    {
                        // Projects "Under Evaluation" by Commission haven't yet AttendeeMusicBandEvaluations
                        // The first AttendeeMusicBandEvaluation is created when Commission evaluate a MusicProject
                        // So, to filter by "UnderEvaluation" status, we need to check if AttendeeMusicBand has no AttendeeMusicBandEvaluations!
                        query = query.Where(mp => !mp.AttendeeMusicBand.AttendeeMusicBandEvaluations.Any());
                    }
                    else
                    {
                        // Filter by selected status in dropdown
                        query = query.Where(mp => mp.AttendeeMusicBand.AttendeeMusicBandEvaluations.Any(ambe => ambe.CommissionEvaluationStatusId == evaluationStatusId));
                    }
                }

                #endregion
            }
            else if (userAccessControlDto.IsCommissionMusicCurator())
            {
                if (editionDto.IsMusicPitchingRepechageEvaluationOpen())
                {
                    #region Projects evaluated by Repechage

                    // Projects Refused by Popular Evaluation
                    query = query.Where(mp => mp.AttendeeMusicBand.AttendeeMusicBandEvaluations.Any(ambe => ambe.PopularEvaluationStatusId == ProjectEvaluationStatus.Refused.Id));

                    if (evaluationStatusId.HasValue)
                    {
                        if (evaluationStatusId == ProjectEvaluationStatus.UnderEvaluation.Id)
                        {
                            query = query.Where(mp => !mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                        .Any(ambe => ambe.RepechageEvaluationStatusId == ProjectEvaluationStatus.Accepted.Id
                                                                    || ambe.RepechageEvaluationStatusId == ProjectEvaluationStatus.Refused.Id));
                        }
                        else
                        {
                            // Filter by selected status in dropdown
                            query = query.Where(mp => mp.AttendeeMusicBand.AttendeeMusicBandEvaluations.Any(ambe => ambe.CuratorEvaluationStatusId == evaluationStatusId));
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Projects evaluated by Curator

                    // Projects Accepted by Commission Evaluation
                    query = query.Where(mp => mp.AttendeeMusicBand.AttendeeMusicBandEvaluations.Any(ambe => ambe.CommissionEvaluationStatusId == ProjectEvaluationStatus.Accepted.Id));

                    if (evaluationStatusId.HasValue)
                    {
                        if (evaluationStatusId == ProjectEvaluationStatus.UnderEvaluation.Id)
                        {
                            query = query.Where(mp => !mp.AttendeeMusicBand.AttendeeMusicBandEvaluations
                                                        .Any(ambe => ambe.CuratorEvaluationStatusId == ProjectEvaluationStatus.Accepted.Id
                                                                    || ambe.CuratorEvaluationStatusId == ProjectEvaluationStatus.Refused.Id));
                        }
                        else
                        {
                            // Filter by selected status in dropdown
                            query = query.Where(mp => mp.AttendeeMusicBand.AttendeeMusicBandEvaluations.Any(ambe => ambe.CuratorEvaluationStatusId == evaluationStatusId));
                        }
                    }

                    #endregion
                }
            }

            return query;
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
        private readonly IUserRepository userRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicProjectRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public MusicProjectRepository(
            Context.PlataformaRio2CContext context,
            IEditionRepository editionRepository,
            IUserRepository userRepository)
            : base(context)
        {
            this.editioRepo = editionRepository;
            this.userRepo = userRepository;
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
        /// Gets the data table base query.
        /// This is a pattern designed by Softo developers to centralize Data Table and Reports query wich shares from the same SearchForm.
        /// It's useful in cases of implementing new filters in the Data Table. It automates the implementation of these same filters for dependent reports as well.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <returns></returns>
        private IQueryable<MusicProject> GetDataTableBaseQuery(int editionId, string searchKeywords, Guid? musicGenreUid, bool showBusinessRounds)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(searchKeywords)
                                .FindByMusicGenreUid(musicGenreUid)
                                .ShowBusinessRounds(showBusinessRounds);

            return query;
        }

        #endregion

        /// <summary>
        /// Finds all dtos to evaluate asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusId">The evaluation status identifier.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicProjectDto>> FindAllDtosPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            int? evaluationStatusId,
            bool showBusinessRounds,
            int page,
            int pageSize,
            int userId)
        {
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var userAccessControlDto = this.userRepo.FindUserAccessControlDtoByUserIdAndByEditionId(userId, editionId);

            var musicProjectsDtos = await this.GetDataTableBaseQuery(editionId, searchKeywords, musicGenreUid, showBusinessRounds)
                                                .FindByEvaluationStatusId(evaluationStatusId, editionDto, userAccessControlDto)
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
                                                })
                                                .Order()
                                                .ToListAsync();

            return await musicProjectsDtos
                .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all by data table asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusId">The evaluation status identifier.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicProjectJsonDto>> FindAllByDataTableAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            int? evaluationStatusId,
            bool showBusinessRounds,
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns,
            int userId)
        {
            this.SetProxyEnabled(false);

            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var userAccessControlDto = this.userRepo.FindUserAccessControlDtoByUserIdAndByEditionId(userId, editionId);

            var musicProjectJsonDtos = await this.GetDataTableBaseQuery(editionId, searchKeywords, musicGenreUid, showBusinessRounds)
                                                    .FindByEvaluationStatusId(evaluationStatusId, editionDto, userAccessControlDto)
                                                    .DynamicOrder(
                                                        sortColumns,
                                                        null,
                                                        new List<string> { "CreateDate", "UpdateDate" },
                                                        "CreateDate")
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
                                                        WouldYouLikeParticipateBusinessRound = mp.AttendeeMusicBand.WouldYouLikeParticipateBusinessRound,
                                                        WouldYouLikeParticipatePitching = mp.AttendeeMusicBand.WouldYouLikeParticipatePitching
                                                    })
                                                    .ToPagedListAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return musicProjectJsonDtos;
        }

        /// <summary>
        /// Finds all music projects report by data table.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusId">The evaluation status identifier.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicProjectReportDto>> FindAllMusicProjectsReportByDataTable(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            int? evaluationStatusId,
            bool showBusinessRounds,
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns,
            int userId)
        {
            this.SetProxyEnabled(false);

            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var userAccessControlDto = this.userRepo.FindUserAccessControlDtoByUserIdAndByEditionId(userId, editionId);

            var pagedMusicProjectReportDtos = await this.GetDataTableBaseQuery(editionId, searchKeywords, musicGenreUid, showBusinessRounds)
                                                    .FindByEvaluationStatusId(evaluationStatusId, editionDto, userAccessControlDto)
                                                    .DynamicOrder(
                                                        sortColumns,
                                                        null,
                                                        new List<string> { "CreateDate", "UpdateDate" }, "CreateDate")
                                                    .Select(mp => new MusicProjectReportDto
                                                    {
                                                        AttendeeMusicBandId = mp.AttendeeMusicBand.Id,
                                                        MusicBandUid = mp.AttendeeMusicBand.MusicBand.Uid,
                                                        MusicBandId = mp.AttendeeMusicBand.MusicBand.Id,
                                                        MusicBandName = mp.AttendeeMusicBand.MusicBand.Name,
                                                        FormationDate = mp.AttendeeMusicBand.MusicBand.FormationDate,
                                                        MainMusicInfluences = mp.AttendeeMusicBand.MusicBand.MainMusicInfluences,
                                                        Facebook = mp.AttendeeMusicBand.MusicBand.Facebook,
                                                        Instagram = mp.AttendeeMusicBand.MusicBand.Instagram,
                                                        Twitter = mp.AttendeeMusicBand.MusicBand.Twitter,
                                                        YouTube = mp.AttendeeMusicBand.MusicBand.Youtube,
                                                        CreateDate = mp.AttendeeMusicBand.MusicBand.CreateDate,
                                                        UpdateDate = mp.AttendeeMusicBand.MusicBand.UpdateDate,
                                                        ImageUploadDate = mp.AttendeeMusicBand.MusicBand.ImageUploadDate,
                                                        WouldYouLikeParticipateBusinessRound = mp.AttendeeMusicBand.WouldYouLikeParticipateBusinessRound,
                                                        WouldYouLikeParticipatePitching = mp.AttendeeMusicBand.WouldYouLikeParticipatePitching,
                                                        MusicBandType = mp.AttendeeMusicBand.MusicBand.MusicBandType,
                                                        MusicProjectApiDto = new MusicProjectApiDto
                                                        {
                                                            Clipping = mp.Clipping1,
                                                            Music1Url = mp.Music1Url,
                                                            Music2Url = mp.Music2Url,
                                                            Release = mp.Release,
                                                            VideoUrl = mp.VideoUrl
                                                        },
                                                        MusicBandResponsibleApiDto = mp.AttendeeMusicBand.AttendeeMusicBandCollaborators.Select(ambc => new MusicBandResponsibleApiDto
                                                        {
                                                            Name = ambc.AttendeeCollaborator.Collaborator.FirstName,
                                                            Email = ambc.AttendeeCollaborator.Collaborator.User.Email,
                                                            Document = ambc.AttendeeCollaborator.Collaborator.Document,
                                                            PhoneNumber = ambc.AttendeeCollaborator.Collaborator.PhoneNumber,
                                                            Country = ambc.AttendeeCollaborator.Collaborator.Address.Country.Name,
                                                            State = ambc.AttendeeCollaborator.Collaborator.Address.State.Name,
                                                            City = ambc.AttendeeCollaborator.Collaborator.Address.City.Name,
                                                            Address = ambc.AttendeeCollaborator.Collaborator.Address.Address1,
                                                            ZipCode = ambc.AttendeeCollaborator.Collaborator.Address.ZipCode
                                                        }).FirstOrDefault(),
                                                        MusicBandMembersApiDtos = mp.AttendeeMusicBand.MusicBand.MusicBandMembers.Select(mbm => new MusicBandMemberApiDto
                                                        {
                                                            Name = mbm.Name,
                                                            MusicInstrumentName = mbm.MusicInstrumentName
                                                        }),
                                                        MusicBandTeamMembersApiDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTeamMembers.Select(mbm => new MusicBandTeamMemberApiDto
                                                        {
                                                            Name = mbm.Name,
                                                            Role = mbm.Role
                                                        }),
                                                        ReleasedMusicProjectsApiDtos = mp.AttendeeMusicBand.MusicBand.ReleasedMusicProjects.Select(rmp => new ReleasedMusicProjectApiDto
                                                        {
                                                            Name = rmp.Name,
                                                            Year = rmp.Year
                                                        }),
                                                        MusicGenresApiDtos = mp.AttendeeMusicBand.MusicBand.MusicBandGenres
                                                                                .Where(mbg => !mbg.IsDeleted && !mbg.MusicGenre.IsDeleted)
                                                                                .OrderBy(mbg => mbg.MusicGenre.DisplayOrder)
                                                                                .Select(mbg => new MusicGenreApiDto
                                                                                {
                                                                                    MusicGenre = mbg.MusicGenre,
                                                                                    AdditionalInfo = mbg.AdditionalInfo
                                                                                }),
                                                        TargetAudiencesApiDtos = mp.AttendeeMusicBand.MusicBand.MusicBandTargetAudiences
                                                                                            .Where(mbg => !mbg.IsDeleted && !mbg.TargetAudience.IsDeleted)
                                                                                            .OrderBy(mbg => mbg.TargetAudience.DisplayOrder)
                                                                                            .Select(mbg => new TargetAudienceApiDto
                                                                                            {
                                                                                                TargetAudience = mbg.TargetAudience
                                                                                            })
                                                    })
                                                    .ToPagedListAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return pagedMusicProjectReportDtos;
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
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
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllApprovedAttendeeMusicBandsIdsAsync(int editionId, int userId)
        {
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var userAccessControlDto = this.userRepo.FindUserAccessControlDtoByUserIdAndByEditionId(userId, editionId);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByEvaluationStatusId(ProjectEvaluationStatus.Accepted.Id, editionDto, userAccessControlDto);

            return await query
                            .Select(mp => mp.AttendeeMusicBandId)
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
        /// <param name="evaluationStatusId">The evaluation status identifier.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllMusicProjectsIdsPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            int? evaluationStatusId,
            bool showBusinessRounds,
            int page,
            int pageSize,
            int userId)
        {
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var userAccessControlDto = this.userRepo.FindUserAccessControlDtoByUserIdAndByEditionId(userId, editionId);

            var pagedMusicProjects = await this.GetDataTableBaseQuery(editionId, searchKeywords, musicGenreUid, showBusinessRounds)
                                            .FindByEvaluationStatusId(evaluationStatusId, editionDto, userAccessControlDto)
                                            .Order()
                                            .ToPagedListAsync(page, pageSize);

            return pagedMusicProjects
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
        /// <param name="evaluationStatusId">The evaluation status uid.</param>
        /// <param name="showBusinessRounds">if set to <c>true</c> [show business rounds].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<int> CountPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? musicGenreUid,
            int? evaluationStatusId,
            bool showBusinessRounds,
            int page,
            int pageSize,
            int userId)
        {
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var userAccessControlDto = this.userRepo.FindUserAccessControlDtoByUserIdAndByEditionId(userId, editionId);

            var pagedMusicProjects = await this.GetDataTableBaseQuery(editionId, searchKeywords, musicGenreUid, showBusinessRounds)
                                            .FindByEvaluationStatusId(evaluationStatusId, editionDto, userAccessControlDto)
                                            .Order()
                                            .ToPagedListAsync(page, pageSize);

            return pagedMusicProjects.Count;
        }

        /// <summary>
        /// Finds the edition count pie widget dto.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
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