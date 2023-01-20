// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-11-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region AttendeeInnovationOrganization IQueryable Extensions

    /// <summary>
    /// AttendeeInnovationOrganizationIQueryableExtensions
    /// </summary>
    internal static class AttendeeInnovationOrganizationIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeInnovationOrganizationsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;AttendeeInnovationOrganization&gt;.</returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByIds(this IQueryable<AttendeeInnovationOrganization> query, List<int?> attendeeInnovationOrganizationsIds)
        {
            if (attendeeInnovationOrganizationsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => attendeeInnovationOrganizationsIds.Contains(aio.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeInnovationOrganizationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByUids(this IQueryable<AttendeeInnovationOrganization> query, List<Guid?> attendeeInnovationOrganizationsUids)
        {
            if (attendeeInnovationOrganizationsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => attendeeInnovationOrganizationsUids.Contains(aio.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> IsNotDeleted(this IQueryable<AttendeeInnovationOrganization> query)
        {
            query = query.Where(aio => !aio.InnovationOrganization.IsDeleted && !aio.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by document.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="document">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>IQueryable&lt;AttendeeInnovationOrganization&gt;.</returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByDocument(this IQueryable<AttendeeInnovationOrganization> query, string document, int editionId)
        {
            document = document.RemoveNonNumeric();

            query = query.Where(aio => aio.InnovationOrganization.Document == document
                                        && aio.EditionId == editionId);

            return query;
        }

        /// <summary>
        /// Finds the by edition identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByEditionId(this IQueryable<AttendeeInnovationOrganization> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(aio => (showAllEditions || aio.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByKeywords(this IQueryable<AttendeeInnovationOrganization> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<AttendeeInnovationOrganization>(false);
                var innerInnovationOrganizationNameWhere = PredicateBuilder.New<AttendeeInnovationOrganization>(true);
                var innerInnovationOrganizationServiceNameWhere = PredicateBuilder.New<AttendeeInnovationOrganization>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerInnovationOrganizationNameWhere = innerInnovationOrganizationNameWhere.Or(aio => aio.InnovationOrganization.Name.Contains(keyword));
                        innerInnovationOrganizationServiceNameWhere = innerInnovationOrganizationServiceNameWhere.Or(aio => aio.InnovationOrganization.ServiceName.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerInnovationOrganizationNameWhere);
                outerWhere = outerWhere.Or(innerInnovationOrganizationServiceNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Finds the by innovation organization track option uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationTrackOptionUids">The innovation organization track option uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByInnovationOrganizationTrackOptionUids(this IQueryable<AttendeeInnovationOrganization> query, List<Guid?> innovationOrganizationTrackOptionUids)
        {
            if (innovationOrganizationTrackOptionUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => innovationOrganizationTrackOptionUids
                                            .Any(iotUid =>
                                                aio.AttendeeInnovationOrganizationTracks
                                                    .Any(aiot =>
                                                            !aio.IsDeleted &&
                                                            !aiot.IsDeleted &&
                                                            !aiot.InnovationOrganizationTrackOption.IsDeleted &&
                                                             aiot.InnovationOrganizationTrackOption.Uid == iotUid)));
            }

            return query;
        }

        /// <summary>
        /// Finds the by innovation organization track option group uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationTrackOptionGroupUids">The innovation organization track option group uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByInnovationOrganizationTrackOptionGroupUids(this IQueryable<AttendeeInnovationOrganization> query, List<Guid?> innovationOrganizationTrackOptionGroupUids)
        {
            if (innovationOrganizationTrackOptionGroupUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => innovationOrganizationTrackOptionGroupUids
                                            .Any(iotUid =>
                                                aio.AttendeeInnovationOrganizationTracks
                                                    .Any(aiot =>
                                                            !aio.IsDeleted &&
                                                            !aiot.IsDeleted &&
                                                            !aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup.IsDeleted &&
                                                             aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup.Uid == iotUid)));
            }

            return query;
        }

        /// <summary>
        /// Finds the by is evaluated.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByIsEvaluated(this IQueryable<AttendeeInnovationOrganization> query)
        {
            query = query.Where(aio => aio.Grade != null);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> Order(this IQueryable<AttendeeInnovationOrganization> query)
        {
            query = query.OrderBy(mp => mp.CreateDate);

            return query;
        }
    }

    #endregion

    #region AttendeeInnovationOrganizationDto IQueryable Extensions

    /// <summary>
    /// AttendeeInnovationOrganizationDtoIQueryableExtensions
    /// </summary>
    internal static class AttendeeInnovationOrganizationDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<AttendeeInnovationOrganizationDto>> ToListPagedAsync(this IQueryable<AttendeeInnovationOrganizationDto> query, int page, int pageSize)
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
        internal static IQueryable<AttendeeInnovationOrganizationDto> Order(this IQueryable<AttendeeInnovationOrganizationDto> query)
        {
            query = query.OrderBy(aioDto => aioDto.AttendeeInnovationOrganization.CreateDate);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeInnovationOrganizationRepository</summary>
    public class AttendeeInnovationOrganizationRepository : Repository<PlataformaRio2CContext, AttendeeInnovationOrganization>, IAttendeeInnovationOrganizationRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeInnovationOrganizationRepository(
            PlataformaRio2CContext context,
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
        private IQueryable<AttendeeInnovationOrganization> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all attendee innovation organizations asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionGroupUids">The innovation organization track option group uids.</param>
        /// <returns></returns>
        private async Task<List<AttendeeInnovationOrganization>> FindAllAttendeeInnovationOrganizationsAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> innovationOrganizationTrackOptionGroupUids)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(searchKeywords)
                                .FindByInnovationOrganizationTrackOptionGroupUids(innovationOrganizationTrackOptionGroupUids);

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all attendee innovation organization dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionUid">The innovation organization track option uid.</param>
        /// <returns></returns> 
        private async Task<List<AttendeeInnovationOrganizationDto>> FindAllAttendeeInnovationOrganizationDtosAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> innovationOrganizationTrackOptionGroupUids)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId)
                               .FindByKeywords(searchKeywords)
                               .FindByInnovationOrganizationTrackOptionGroupUids(innovationOrganizationTrackOptionGroupUids)
                               .Select(aio => new AttendeeInnovationOrganizationDto
                               {
                                   AttendeeInnovationOrganization = aio,
                                   InnovationOrganization = aio.InnovationOrganization,
                                   AttendeeInnovationOrganizationCollaboratorDtos = aio.AttendeeInnovationOrganizationCollaborators
                                                                                       .Where(aioc => !aioc.IsDeleted)
                                                                                       .Select(aioc =>
                                                                                       new AttendeeInnovationOrganizationCollaboratorDto
                                                                                       {
                                                                                           AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                           Collaborator = aioc.AttendeeCollaborator.Collaborator
                                                                                       }),
                                   AttendeeInnovationOrganizationCompetitorDtos = aio.AttendeeInnovationOrganizationCompetitors.Select(aioc =>
                                                                                       new AttendeeInnovationOrganizationCompetitorDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationCompetitor = aioc
                                                                                       }),
                                   AttendeeInnovationOrganizationFounderDtos = aio.AttendeeInnovationOrganizationFounders.Select(aiof =>
                                                                                       new AttendeeInnovationOrganizationFounderDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationFounder = aiof
                                                                                       }),
                                   AttendeeInnovationOrganizationExperienceDtos = aio.AttendeeInnovationOrganizationExperiences.Select(aioe =>
                                                                                       new AttendeeInnovationOrganizationExperienceDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationExperience = aioe,
                                                                                           InnovationOrganizationExperienceOption = aioe.InnovationOrganizationExperienceOption
                                                                                       }),
                                   AttendeeInnovationOrganizationObjectiveDtos = aio.AttendeeInnovationOrganizationObjectives.Select(aioo =>
                                                                                       new AttendeeInnovationOrganizationObjectiveDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationObjective = aioo,
                                                                                           InnovationOrganizationObjectivesOption = aioo.InnovationOrganizationObjectivesOption
                                                                                       }),
                                   AttendeeInnovationOrganizationTechnologyDtos = aio.AttendeeInnovationOrganizationTechnologies.Select(aiot =>
                                                                                       new AttendeeInnovationOrganizationTechnologyDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationTechnology = aiot,
                                                                                           InnovationOrganizationTechnologyOption = aiot.InnovationOrganizationTechnologyOption
                                                                                       }),
                                   AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                       new AttendeeInnovationOrganizationTrackDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationTrack = aiot,
                                                                                           InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                           InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                       }),
                                   AttendeeInnovationOrganizationEvaluationDtos = aio.AttendeeInnovationOrganizationEvaluations
                                                                                      .Where(aioe => !aioe.IsDeleted)
                                                                                      .Select(aioe => new AttendeeInnovationOrganizationEvaluationDto
                                                                                      {
                                                                                          AttendeeInnovationOrganizationEvaluation = aioe,
                                                                                          EvaluatorUser = aioe.EvaluatorUser
                                                                                      }).ToList()
                               });

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all json dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionUid">The innovation organization track option uid.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        private async Task<List<AttendeeInnovationOrganizationJsonDto>> FindAllJsonDtosAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> innovationOrganizationTrackOptionGroupsUids,
            List<Tuple<string, string>> sortColumns)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId, false)
                               .FindByKeywords(searchKeywords)
                               .FindByInnovationOrganizationTrackOptionGroupUids(innovationOrganizationTrackOptionGroupsUids)
                               .DynamicOrder<AttendeeInnovationOrganization>(
                                   sortColumns,
                                   null,
                                   new List<string> { "CreateDate", "UpdateDate" },
                                   "CreateDate")
                               .Select(aio => new AttendeeInnovationOrganizationJsonDto
                               {
                                   AttendeeInnovationOrganizationId = aio.Id,
                                   AttendeeInnovationOrganizationUid = aio.Uid,
                                   InnovationOrganizationId = aio.InnovationOrganization.Id,
                                   InnovationOrganizationUid = aio.InnovationOrganization.Uid,
                                   InnovationOrganizationName = aio.InnovationOrganization.Name,
                                   InnovationOrganizationServiceName = aio.InnovationOrganization.ServiceName,
                                   ImageUploadDate = aio.InnovationOrganization.ImageUploadDate,
                                   Grade = aio.Grade,
                                   EvaluationsCount = aio.AttendeeInnovationOrganizationEvaluations.Count(aioe => !aioe.IsDeleted),
                                   InnovationOrganizationTracksNames = aio.AttendeeInnovationOrganizationTracks
                                                                            .Where(aiot => !aio.IsDeleted && !aiot.IsDeleted && !aiot.InnovationOrganizationTrackOption.IsDeleted)
                                                                            .OrderBy(aiot => aiot.InnovationOrganizationTrackOption.DisplayOrder)
                                                                            .Select(aiot => aiot.InnovationOrganizationTrackOption.Name)
                                                                            .ToList(),

                                   InnovationOrganizationTrackOptionGroupDtos = aio.AttendeeInnovationOrganizationTracks
                                                                                    .Where(aiot => !aio.IsDeleted &&
                                                                                                    !aiot.IsDeleted &&
                                                                                                    !aiot.InnovationOrganizationTrackOption.IsDeleted &&
                                                                                                    (aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup != null ?
                                                                                                        !aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup.IsDeleted :
                                                                                                        true))
                                                                                    .OrderBy(aiot => aiot.InnovationOrganizationTrackOption.DisplayOrder)
                                                                                    .GroupBy(aiot => aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup.Name)
                                                                                    .Select(aiotg => new InnovationOrganizationTrackOptionGroupDto
                                                                                    {
                                                                                        GroupName = aiotg.Key,
                                                                                        InnovationOrganizationTrackOptionNames = aiotg.Select(s => s.InnovationOrganizationTrackOption.Name)
                                                                                    })
                                                                                    .ToList(),
                                   CreateDate = aio.CreateDate,
                                   UpdateDate = aio.UpdateDate
                               });

            return await query
                            .ToListAsync();
        }

        #endregion

        /// <summary>
        /// Finds all music project dtos paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionUid">The innovation organization track option uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IPagedList<AttendeeInnovationOrganizationDto>> FindAllDtosPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> innovationOrganizationTrackOptionGroupUids,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            var attendeeInnovationOrganizationsDtos = await this.FindAllAttendeeInnovationOrganizationDtosAsync(editionId, searchKeywords, innovationOrganizationTrackOptionGroupUids);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeInnovationOrganizationDto> attendeeInnovationOrganizationDtosResult = attendeeInnovationOrganizationsDtos;
            if (editionDto.IsInnovationProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationDtosResult = new List<AttendeeInnovationOrganizationDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationDtosResult = new List<AttendeeInnovationOrganizationDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedAttendeeInnovationOrganizationsIds = await this.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationDtosResult = attendeeInnovationOrganizationsDtos.Where(aioDto => approvedAttendeeInnovationOrganizationsIds.Contains(aioDto.AttendeeInnovationOrganization.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationDtosResult = attendeeInnovationOrganizationsDtos.Where(aioDto => !approvedAttendeeInnovationOrganizationsIds.Contains(aioDto.AttendeeInnovationOrganization.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeInnovationOrganizationDtosResult = new List<AttendeeInnovationOrganizationDto>();
                }

                #endregion
            }

            return await attendeeInnovationOrganizationDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all json dtos paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionGroupUids">The innovation organization track option group uids.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        public async Task<IPagedList<AttendeeInnovationOrganizationJsonDto>> FindAllJsonDtosPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> innovationOrganizationTrackOptionGroupUids,
            Guid? evaluationStatusUid,
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns)
        {
            var attendeeInnovaitonOrganizationJsonDtos = await this.FindAllJsonDtosAsync(editionId, searchKeywords, innovationOrganizationTrackOptionGroupUids, sortColumns);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeInnovationOrganizationJsonDto> attendeeInnovationOrganizationJsonDtosResult = attendeeInnovaitonOrganizationJsonDtos;
            if (editionDto.IsInnovationProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationJsonDtosResult = new List<AttendeeInnovationOrganizationJsonDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationJsonDtosResult = new List<AttendeeInnovationOrganizationJsonDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedInnovationOrganizationsIds = await this.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationJsonDtosResult = attendeeInnovaitonOrganizationJsonDtos.Where(w => approvedInnovationOrganizationsIds.Contains(w.AttendeeInnovationOrganizationId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationJsonDtosResult = attendeeInnovaitonOrganizationJsonDtos.Where(w => !approvedInnovationOrganizationsIds.Contains(w.AttendeeInnovationOrganizationId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeInnovationOrganizationJsonDtosResult = new List<AttendeeInnovationOrganizationJsonDto>();
                }

                #endregion
            }

            return await attendeeInnovationOrganizationJsonDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<AttendeeInnovationOrganization> FindByIdAsync(int attendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { attendeeInnovationOrganizationIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The innovation organization uid.</param>
        /// <returns>Task&lt;AttendeeInnovationOrganization&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganization> FindByUidAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganization>> FindAllByIdsAsync(List<int?> attendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(attendeeInnovationOrganizationIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganization>> FindAllByUidsAsync(List<Guid?> attendeeInnovationOrganizationUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(attendeeInnovationOrganizationUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by document and edition identifier as an asynchronous operation.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>Task&lt;AttendeeInnovationOrganization&gt;.</returns>
        public async Task<AttendeeInnovationOrganization> FindByDocumentAndEditionIdAsync(string document, int editionId)
        {
            var query = this.GetBaseQuery()
                           .FindByDocument(document, editionId);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all approved attendee innovation organizations ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(int editionId)
        {
            var edition = await this.editioRepo.FindByIdAsync(editionId);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByIsEvaluated();

            return await query
                            .OrderByDescending(aio => aio.Grade)
                            .Take(edition.InnovationCommissionMaximumApprovedCompaniesCount)
                            .Select(aio => aio.Id)
                            .ToArrayAsync();
        }

        /// <summary>
        /// Finds all innovation organizations ids paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionGroupUids">The innovation organization track option group uids.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllInnovationOrganizationsIdsPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> innovationOrganizationTrackOptionGroupUids,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            var attendeeInnovationOrganizations = await this.FindAllAttendeeInnovationOrganizationsAsync(editionId, searchKeywords, innovationOrganizationTrackOptionGroupUids);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeInnovationOrganization> attendeeInnovationOrganizationResult = attendeeInnovationOrganizations;
            if (editionDto.IsInnovationProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationResult = new List<AttendeeInnovationOrganization>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationResult = new List<AttendeeInnovationOrganization>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedAttendeeInnovationOrganizationsIds = await this.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationResult = attendeeInnovationOrganizations.Where(aio => approvedAttendeeInnovationOrganizationsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationResult = attendeeInnovationOrganizations.Where(aio => !approvedAttendeeInnovationOrganizationsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeInnovationOrganizationResult = new List<AttendeeInnovationOrganization>();
                }

                #endregion
            }

            var attendeeInnovationOrganizationsPagedList = await attendeeInnovationOrganizationResult.ToPagedListAsync(page, pageSize);
            return attendeeInnovationOrganizationsPagedList
                            .Select(aio => aio.Id)
                            .OrderBy(aioId => aioId)
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
        /// Counts the paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionGroupUids">The innovation organization track option group uids.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int> CountPagedAsync(int editionId, string searchKeywords, List<Guid?> innovationOrganizationTrackOptionGroupUids, Guid? evaluationStatusUid, int page, int pageSize)
        {
            var attendeeInnovationOrganizations = await this.FindAllAttendeeInnovationOrganizationsAsync(editionId, searchKeywords, innovationOrganizationTrackOptionGroupUids);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedAttendeeInnovationOrganizationsIds = await this.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(editionId);

            IEnumerable<AttendeeInnovationOrganization> attendeeInnovationOrganizationsResult = attendeeInnovationOrganizations;
            if (editionDto.IsInnovationProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationsResult = new List<AttendeeInnovationOrganization>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationsResult = new List<AttendeeInnovationOrganization>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovationOrganizationsResult = attendeeInnovationOrganizations.Where(aio => approvedAttendeeInnovationOrganizationsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovationOrganizationsResult = attendeeInnovationOrganizations.Where(aio => !approvedAttendeeInnovationOrganizationsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeInnovationOrganizationsResult = new List<AttendeeInnovationOrganization>();
                }

                #endregion
            }

            var attendeeInnovationOrganizationsPagedList = await attendeeInnovationOrganizationsResult
                                                 .ToPagedListAsync(page, pageSize);

            return attendeeInnovationOrganizationsPagedList.Count;
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationId">The attendee innovation organization identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganizationDto> FindDtoToEvaluateAsync(int attendeeInnovationOrganizationId)
        {
            var query = this.GetBaseQuery()
                               .FindByIds(new List<int?> { attendeeInnovationOrganizationId })
                               .Select(aio => new AttendeeInnovationOrganizationDto
                               {
                                   AttendeeInnovationOrganization = aio,
                                   InnovationOrganization = aio.InnovationOrganization,
                                   AttendeeInnovationOrganizationCollaboratorDtos = aio.AttendeeInnovationOrganizationCollaborators
                                                                                       .Where(aioc => !aioc.IsDeleted)
                                                                                       .Select(aioc =>
                                                                                       new AttendeeInnovationOrganizationCollaboratorDto
                                                                                       {
                                                                                           AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                           Collaborator = aioc.AttendeeCollaborator.Collaborator
                                                                                       }),
                                   AttendeeInnovationOrganizationCompetitorDtos = aio.AttendeeInnovationOrganizationCompetitors.Select(aioc =>
                                                                                       new AttendeeInnovationOrganizationCompetitorDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationCompetitor = aioc
                                                                                       }),
                                   AttendeeInnovationOrganizationFounderDtos = aio.AttendeeInnovationOrganizationFounders.Select(aiof =>
                                                                                       new AttendeeInnovationOrganizationFounderDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationFounder = aiof
                                                                                       }),
                                   AttendeeInnovationOrganizationExperienceDtos = aio.AttendeeInnovationOrganizationExperiences.Select(aioe =>
                                                                                       new AttendeeInnovationOrganizationExperienceDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationExperience = aioe,
                                                                                           InnovationOrganizationExperienceOption = aioe.InnovationOrganizationExperienceOption
                                                                                       }),
                                   AttendeeInnovationOrganizationObjectiveDtos = aio.AttendeeInnovationOrganizationObjectives.Select(aioo =>
                                                                                       new AttendeeInnovationOrganizationObjectiveDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationObjective = aioo,
                                                                                           InnovationOrganizationObjectivesOption = aioo.InnovationOrganizationObjectivesOption
                                                                                       }),
                                   AttendeeInnovationOrganizationTechnologyDtos = aio.AttendeeInnovationOrganizationTechnologies.Select(aiot =>
                                                                                       new AttendeeInnovationOrganizationTechnologyDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationTechnology = aiot,
                                                                                           InnovationOrganizationTechnologyOption = aiot.InnovationOrganizationTechnologyOption
                                                                                       }),
                                   AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                       new AttendeeInnovationOrganizationTrackDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationTrack = aiot,
                                                                                           InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                           InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                       })
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganizationDto> FindDtoToEvaluateAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                                .Select(aio => new AttendeeInnovationOrganizationDto
                                {
                                    AttendeeInnovationOrganization = aio,
                                    InnovationOrganization = aio.InnovationOrganization,
                                    AttendeeInnovationOrganizationCollaboratorDtos = aio.AttendeeInnovationOrganizationCollaborators
                                                                                        .Where(aioc => !aioc.IsDeleted)
                                                                                        .Select(aioc =>
                                                                                        new AttendeeInnovationOrganizationCollaboratorDto
                                                                                        {
                                                                                            AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                            Collaborator = aioc.AttendeeCollaborator.Collaborator
                                                                                        }),
                                    AttendeeInnovationOrganizationCompetitorDtos = aio.AttendeeInnovationOrganizationCompetitors.Select(aioc =>
                                                                                        new AttendeeInnovationOrganizationCompetitorDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationCompetitor = aioc
                                                                                        }),
                                    AttendeeInnovationOrganizationFounderDtos = aio.AttendeeInnovationOrganizationFounders.Select(aiof =>
                                                                                        new AttendeeInnovationOrganizationFounderDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationFounder = aiof
                                                                                        }),
                                    AttendeeInnovationOrganizationExperienceDtos = aio.AttendeeInnovationOrganizationExperiences.Select(aioe =>
                                                                                        new AttendeeInnovationOrganizationExperienceDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationExperience = aioe,
                                                                                            InnovationOrganizationExperienceOption = aioe.InnovationOrganizationExperienceOption
                                                                                        }),
                                    AttendeeInnovationOrganizationObjectiveDtos = aio.AttendeeInnovationOrganizationObjectives.Select(aioo =>
                                                                                        new AttendeeInnovationOrganizationObjectiveDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationObjective = aioo,
                                                                                            InnovationOrganizationObjectivesOption = aioo.InnovationOrganizationObjectivesOption
                                                                                        }),
                                    AttendeeInnovationOrganizationTechnologyDtos = aio.AttendeeInnovationOrganizationTechnologies.Select(aiot =>
                                                                                        new AttendeeInnovationOrganizationTechnologyDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationTechnology = aiot,
                                                                                            InnovationOrganizationTechnologyOption = aiot.InnovationOrganizationTechnologyOption
                                                                                        }),
                                    AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                        new AttendeeInnovationOrganizationTrackDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationTrack = aiot,
                                                                                            InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                            InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                        })
                                });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the main information widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganizationDto> FindMainInformationWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                               .Select(aio => new AttendeeInnovationOrganizationDto
                               {
                                   AttendeeInnovationOrganization = aio,
                                   InnovationOrganization = aio.InnovationOrganization,
                                   AttendeeInnovationOrganizationCollaboratorDtos = aio.AttendeeInnovationOrganizationCollaborators
                                                                                   .Where(aioc => !aioc.IsDeleted)
                                                                                   .Select(aioc => new AttendeeInnovationOrganizationCollaboratorDto
                                                                                   {
                                                                                       AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                       Collaborator = aioc.AttendeeCollaborator.Collaborator,
                                                                                   }).ToList(),
                                   AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                     new AttendeeInnovationOrganizationTrackDto
                                                                                     {
                                                                                         AttendeeInnovationOrganizationTrack = aiot,
                                                                                         InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                         InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                     }),
                                   AttendeeInnovationOrganizationEvaluationDtos = aio.AttendeeInnovationOrganizationEvaluations
                                                                                      .Where(aioe => !aioe.IsDeleted)
                                                                                      .Select(aioe => new AttendeeInnovationOrganizationEvaluationDto
                                                                                      {
                                                                                          AttendeeInnovationOrganizationEvaluation = aioe,
                                                                                          EvaluatorUser = aioe.EvaluatorUser
                                                                                      }).ToList()
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the business information widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindBusinessInformationWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                               .Select(aio => new AttendeeInnovationOrganizationDto
                               {
                                   AttendeeInnovationOrganization = aio,
                                   InnovationOrganization = aio.InnovationOrganization,
                                   AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                       new AttendeeInnovationOrganizationTrackDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationTrack = aiot,
                                                                                           InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                           InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                       }),
                                   AttendeeInnovationOrganizationCompetitorDtos = aio.AttendeeInnovationOrganizationCompetitors.Select(aiot =>
                                                                                     new AttendeeInnovationOrganizationCompetitorDto
                                                                                     {
                                                                                         AttendeeInnovationOrganization = aio,
                                                                                         AttendeeInnovationOrganizationCompetitor = aiot
                                                                                     })
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the tracks widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganizationDto> FindTracksWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                              .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                              .Select(aio => new AttendeeInnovationOrganizationDto
                              {
                                  AttendeeInnovationOrganization = aio,
                                  InnovationOrganization = aio.InnovationOrganization,
                                  AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                      new AttendeeInnovationOrganizationTrackDto
                                                                                      {
                                                                                          AttendeeInnovationOrganizationTrack = aiot,
                                                                                          InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                          InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                      })
                              });


            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the objectives widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindObjectivesWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                             .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                             .Select(aio => new AttendeeInnovationOrganizationDto
                             {
                                 AttendeeInnovationOrganization = aio,
                                 InnovationOrganization = aio.InnovationOrganization,
                                 AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                     new AttendeeInnovationOrganizationTrackDto
                                                                                     {
                                                                                         AttendeeInnovationOrganizationTrack = aiot,
                                                                                         InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                         InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                     }),
                                 AttendeeInnovationOrganizationObjectiveDtos = aio.AttendeeInnovationOrganizationObjectives.Select(aioo =>
                                                                                         new AttendeeInnovationOrganizationObjectiveDto
                                                                                         {
                                                                                             AttendeeInnovationOrganizationObjective = aioo,
                                                                                             InnovationOrganizationObjectivesOption = aioo.InnovationOrganizationObjectivesOption
                                                                                         })
                             });


            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the sustainable development widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindSustainableDevelopmentWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                              .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                              .Select(aio => new AttendeeInnovationOrganizationDto
                              {
                                  AttendeeInnovationOrganization = aio,
                                  InnovationOrganization = aio.InnovationOrganization,
                                  AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto = aio.AttendeeInnovationOrganizationSustainableDevelopmentObjective.Select(aiosdo =>
                                  new AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto
                                  {
                                      AttendeeInnovationOrganizationSustainableDevelopmentObjective = aiosdo,
                                      InnovationOrganizationSustainableDevelopmentObjectivesOption = aiosdo.InnovationOrganizationSustainableDevelopmentObjectiveOption
                                  })
                              });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the experiences widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindExperiencesWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                             .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                             .Select(aio => new AttendeeInnovationOrganizationDto
                             {
                                 AttendeeInnovationOrganization = aio,
                                 InnovationOrganization = aio.InnovationOrganization,
                                 AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks.Select(aiot =>
                                                                                     new AttendeeInnovationOrganizationTrackDto
                                                                                     {
                                                                                         AttendeeInnovationOrganizationTrack = aiot,
                                                                                         InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                         InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                     }),
                                 AttendeeInnovationOrganizationExperienceDtos = aio.AttendeeInnovationOrganizationExperiences.Select(aioe =>
                                                                                        new AttendeeInnovationOrganizationExperienceDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationExperience = aioe,
                                                                                            InnovationOrganizationExperienceOption = aioe.InnovationOrganizationExperienceOption
                                                                                        }),
                             });


            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the technologies widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindTechnologiesWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                            .Select(aio => new AttendeeInnovationOrganizationDto
                            {
                                AttendeeInnovationOrganization = aio,
                                InnovationOrganization = aio.InnovationOrganization,
                                AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks
                                                                                    .Where(aiot => !aiot.IsDeleted)
                                                                                    .Select(aiot =>
                                                                                     new AttendeeInnovationOrganizationTrackDto
                                                                                     {
                                                                                         AttendeeInnovationOrganizationTrack = aiot,
                                                                                         InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                         InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                     }),
                                AttendeeInnovationOrganizationTechnologyDtos = aio.AttendeeInnovationOrganizationTechnologies
                                                                                        .Where(aiot => !aiot.IsDeleted)
                                                                                        .Select(aiot =>
                                                                                        new AttendeeInnovationOrganizationTechnologyDto
                                                                                        {
                                                                                            AttendeeInnovationOrganizationTechnology = aiot,
                                                                                            InnovationOrganizationTechnologyOption = aiot.InnovationOrganizationTechnologyOption
                                                                                        }),
                            });


            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the evaluation grade widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindEvaluationGradeWidgetDtoAsync(Guid attendeeInnovationOrganizationUid, int userId)
        {
            var query = this.GetBaseQuery()
                              .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                              .Select(aio => new AttendeeInnovationOrganizationDto
                              {
                                  AttendeeInnovationOrganization = aio,
                                  InnovationOrganization = aio.InnovationOrganization,
                                  AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks
                                                                                    .Where(aiot => !aiot.IsDeleted)
                                                                                    .Select(aiot =>
                                                                                     new AttendeeInnovationOrganizationTrackDto
                                                                                     {
                                                                                         AttendeeInnovationOrganizationTrack = aiot,
                                                                                         InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                         InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                     }),
                                  AttendeeInnovationOrganizationEvaluationDtos = aio.AttendeeInnovationOrganizationEvaluations
                                                                                      .Where(aioe => !aioe.IsDeleted)
                                                                                      .Select(aioe => new AttendeeInnovationOrganizationEvaluationDto
                                                                                      {
                                                                                          AttendeeInnovationOrganizationEvaluation = aioe,
                                                                                          EvaluatorUser = aioe.EvaluatorUser
                                                                                      }).ToList(),
                              //Current AttendeeInnovationOrganizationEvaluation by user Id
                              AttendeeInnovationOrganizationEvaluationDto = aio.AttendeeInnovationOrganizationEvaluations
                                                                                   .Where(aioe => !aioe.IsDeleted && aioe.EvaluatorUserId == userId)
                                                                                   .Select(aioe => new AttendeeInnovationOrganizationEvaluationDto
                                                                                   {
                                                                                       AttendeeInnovationOrganizationEvaluation = aioe,
                                                                                       EvaluatorUser = aioe.EvaluatorUser
                                                                                   }).FirstOrDefault()
                              });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the evaluators widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindEvaluatorsWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                              .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                              .Select(aio => new AttendeeInnovationOrganizationDto
                              {
                                  AttendeeInnovationOrganization = aio,
                                  InnovationOrganization = aio.InnovationOrganization,
                                  AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks
                                                                                    .Where(aiot => !aiot.IsDeleted)
                                                                                    .Select(aiot =>
                                                                                     new AttendeeInnovationOrganizationTrackDto
                                                                                     {
                                                                                         AttendeeInnovationOrganizationTrack = aiot,
                                                                                         InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                         InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                     }),
                                  AttendeeInnovationOrganizationEvaluationDtos = aio.AttendeeInnovationOrganizationEvaluations
                                                                                        .Where(aioe => !aioe.IsDeleted)
                                                                                        .Select(aioe =>
                                                                                          new AttendeeInnovationOrganizationEvaluationDto
                                                                                          {
                                                                                              AttendeeInnovationOrganizationEvaluation = aioe,
                                                                                              AttendeeInnovationOrganization = aioe.AttendeeInnovationOrganization,
                                                                                              EvaluatorUser = aioe.EvaluatorUser
                                                                                          })
                              });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the founders widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindFoundersWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                             .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                             .Select(aio => new AttendeeInnovationOrganizationDto
                             {
                                 AttendeeInnovationOrganization = aio,
                                 InnovationOrganization = aio.InnovationOrganization,
                                 AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks
                                                                                    .Where(aiot => !aiot.IsDeleted)
                                                                                    .Select(aiot =>
                                                                                    new AttendeeInnovationOrganizationTrackDto
                                                                                    {
                                                                                        AttendeeInnovationOrganizationTrack = aiot,
                                                                                        InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                        InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                    }),
                                 AttendeeInnovationOrganizationFounderDtos = aio.AttendeeInnovationOrganizationFounders
                                                                                     .Where(aiot => !aiot.IsDeleted)
                                                                                     .Select(aiof => new AttendeeInnovationOrganizationFounderDto
                                                                                     {
                                                                                         AttendeeInnovationOrganization = aio,
                                                                                         AttendeeInnovationOrganizationFounder = aiof
                                                                                     })
                             });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the clipping widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The attendee innovation organization uid.</param>
        /// <returns></returns>
        public async Task<AttendeeInnovationOrganizationDto> FindPresentationWidgetDtoAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                             .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid })
                             .Select(aio => new AttendeeInnovationOrganizationDto
                             {
                                 AttendeeInnovationOrganization = aio,
                                 InnovationOrganization = aio.InnovationOrganization,
                                 AttendeeInnovationOrganizationTrackDtos = aio.AttendeeInnovationOrganizationTracks
                                                                                     .Where(aiot => !aiot.IsDeleted)
                                                                                     .Select(aiot =>
                                                                                       new AttendeeInnovationOrganizationTrackDto
                                                                                       {
                                                                                           AttendeeInnovationOrganizationTrack = aiot,
                                                                                           InnovationOrganizationTrackOption = aiot.InnovationOrganizationTrackOption,
                                                                                           InnovationOrganizationTrackOptionGroup = aiot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup
                                                                                       })
                             });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the edition count chart widget dto.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<InnovationOrganizationGroupedByTrackDto>> FindEditionCountPieWidgetDto(int editionId)
        {
            var query = this.GetBaseQuery()
                                .IsNotDeleted()
                                .FindByEditionId(editionId, false);

            return await query.SelectMany(aio => aio.AttendeeInnovationOrganizationTracks)
                                    .GroupBy(aiot => aiot.InnovationOrganizationTrackOption.Name)
                                    .Select(innovationOrganizationTracksGroupedByTrackName => new InnovationOrganizationGroupedByTrackDto
                                    {
                                        TrackName = innovationOrganizationTracksGroupedByTrackName.Key,
                                        InnovationProjectsTotalCount = innovationOrganizationTracksGroupedByTrackName.Count()
                                    })
                                    .OrderByDescending(iog => iog.InnovationProjectsTotalCount)
                                    .ToListAsync();
        }

        /// <summary>
        /// Finds all by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<AttendeeInnovationOrganization>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId);

            return await query
                            .ToListAsync();
        }
    }
}