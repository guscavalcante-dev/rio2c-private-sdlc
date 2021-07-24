// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
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
                query = query.Where(ao => attendeeInnovationOrganizationsIds.Contains(ao.Id));
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
            query = query.Where(aio => !aio.IsDeleted);

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
                query = query.Where(aio => innovationOrganizationTrackOptionUids.Any(iotUid =>
                                                    aio.AttendeeInnovationOrganizationTracks.Any(aiot => 
                                                        !aiot.IsDeleted &&
                                                        !aiot.InnovationOrganizationTrackOption.IsDeleted &&
                                                         aiot.InnovationOrganizationTrackOption.Uid == iotUid)));
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


        private async Task<List<AttendeeInnovationOrganization>> FindAllAttendeeInnovationOrganizationsAsync(int editionId, string searchKeywords, Guid? innovationOrganizationTrackOptionUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(searchKeywords)
                                .FindByInnovationOrganizationTrackOptionUids(new List<Guid?> { innovationOrganizationTrackOptionUid });

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
        private async Task<List<AttendeeInnovationOrganizationJsonDto>> FindAllJsonDtosAsync(int editionId, string searchKeywords, Guid? innovationOrganizationTrackOptionUid, List<Tuple<string, string>> sortColumns)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId, false)
                               .FindByKeywords(searchKeywords)
                               .FindByInnovationOrganizationTrackOptionUids(new List<Guid?> { innovationOrganizationTrackOptionUid })
                               .DynamicOrder<AttendeeInnovationOrganization>(
                                   sortColumns,
                                   null,
                                   new List<string> { "CreateDate", "UpdateDate" }, 
                                   "CreateDate")
                               .Select(aio => new AttendeeInnovationOrganizationJsonDto
                               {
                                   AttendeeInnovationOrganizationId = aio.Id,
                                   AttendeeInnovationOrganizationUid = aio.Uid,
                                   
                                   InnovationOrganizationName = aio.InnovationOrganization.Name,
                                   InnovationOrganizationServiceName = aio.InnovationOrganization.ServiceName,
                                   //InnovationOrganizationImageUrl = aio.InnovationOrganization.ImageUrl,
                                   Grade = aio.Grade,
                                   EvaluationsCount = aio.EvaluationsCount,
                                   InnovationOrganizationTracksNames = aio.AttendeeInnovationOrganizationTracks
                                                                            .Where(aiot => !aio.IsDeleted && 
                                                                                            !aiot.IsDeleted && 
                                                                                            !aiot.InnovationOrganizationTrackOption.IsDeleted)
                                                                            .OrderBy(aiot => aiot.InnovationOrganizationTrackOption.DisplayOrder)
                                                                            .Select(aiot => aiot.InnovationOrganizationTrackOption.Name).ToList(),

                                   CreateDate = aio.CreateDate,
                                   UpdateDate = aio.UpdateDate
                               });

            return await query
                            .ToListAsync();
        }

        #endregion

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<AttendeeInnovationOrganization> FindByIdAsync(int AttendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { AttendeeInnovationOrganizationIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationUid">The innovation organization uid.</param>
        /// <returns>Task&lt;AttendeeInnovationOrganization&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganization> FindByUidAsync(Guid AttendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { AttendeeInnovationOrganizationUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganization>> FindAllByIdsAsync(List<int?> AttendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(AttendeeInnovationOrganizationIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganization>> FindAllByUidsAsync(List<Guid?> AttendeeInnovationOrganizationUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(AttendeeInnovationOrganizationUids);

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
        /// Finds all json dtos paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionUid">The innovation organization track option uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        public async Task<IPagedList<AttendeeInnovationOrganizationJsonDto>> FindAllJsonDtosPagedAsync(int editionId, string searchKeywords, Guid? innovationOrganizationTrackOptionUid, Guid? evaluationStatusUid, int page, int pageSize, List<Tuple<string, string>> sortColumns)
        {
            var attendeeInnovaitonOrganizationJsonDtos = await this.FindAllJsonDtosAsync(editionId, searchKeywords, innovationOrganizationTrackOptionUid, sortColumns);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedMusicBandsIds = await this.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(editionId);

            IEnumerable<AttendeeInnovationOrganizationJsonDto> attendeeInnovaitonOrganizationJsonDtosResult = attendeeInnovaitonOrganizationJsonDtos;
            if (editionDto.IsInnovationProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovaitonOrganizationJsonDtosResult = new List<AttendeeInnovationOrganizationJsonDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovaitonOrganizationJsonDtosResult = new List<AttendeeInnovationOrganizationJsonDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeInnovaitonOrganizationJsonDtosResult = attendeeInnovaitonOrganizationJsonDtos.Where(w => approvedMusicBandsIds.Contains(w.AttendeeInnovationOrganizationId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeInnovaitonOrganizationJsonDtosResult = attendeeInnovaitonOrganizationJsonDtos.Where(w => !approvedMusicBandsIds.Contains(w.AttendeeInnovationOrganizationId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeInnovaitonOrganizationJsonDtosResult = new List<AttendeeInnovationOrganizationJsonDto>();
                }

                #endregion
            }

            return await attendeeInnovaitonOrganizationJsonDtosResult
                            .ToPagedListAsync(page, pageSize);
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
                            .Take(edition.InnovationProjectMaximumApprovedCompaniesCount)
                            .Select(aio => aio.Id)
                            .ToArrayAsync();
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
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int> CountPagedAsync(int editionId, string searchKeywords, Guid? musicGenreUid, Guid? evaluationStatusUid, int page, int pageSize)
        {
            var attendeeInnovationOrganizations = await this.FindAllAttendeeInnovationOrganizationsAsync(editionId, searchKeywords, musicGenreUid);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedAttendeeInnovationOrganizationsIds = await this.FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(editionId);

            IEnumerable<AttendeeInnovationOrganization> attendeeInnovationOrganizationsResult = attendeeInnovationOrganizations;
            if (editionDto.IsMusicProjectEvaluationOpen())
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
    }
}