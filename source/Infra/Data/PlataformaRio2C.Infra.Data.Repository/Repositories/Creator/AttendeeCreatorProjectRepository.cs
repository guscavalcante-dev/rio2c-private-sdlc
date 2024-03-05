// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProjectRepository.cs" company="Softo">
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
    #region AttendeeCreatorProject IQueryable Extensions

    /// <summary>
    /// AttendeeCreatorProjectIQueryableExtensions
    /// </summary>
    internal static class AttendeeCreatorProjectIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCreatorProjectIds">The attendee creator project ids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProject> FindByIds(this IQueryable<AttendeeCreatorProject> query, List<int?> attendeeCreatorProjectIds)
        {
            if (attendeeCreatorProjectIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(acp => attendeeCreatorProjectIds.Contains(acp.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCreatorProjectUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProject> FindByUids(this IQueryable<AttendeeCreatorProject> query, List<Guid?> attendeeCreatorProjectUids)
        {
            if (attendeeCreatorProjectUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(acp => attendeeCreatorProjectUids.Contains(acp.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProject> IsNotDeleted(this IQueryable<AttendeeCreatorProject> query)
        {
            query = query.Where(acp => !acp.CreatorProject.IsDeleted && !acp.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by document.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="title">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>IQueryable&lt;AttendeeCreatorProject&gt;.</returns>
        internal static IQueryable<AttendeeCreatorProject> FindByTitleAndEditionId(this IQueryable<AttendeeCreatorProject> query, string title, int editionId)
        {
            query = query.Where(acp => acp.CreatorProject.Title == title
                                        && acp.EditionId == editionId);

            return query;
        }

        /// <summary>
        /// Finds the by edition identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProject> FindByEditionId(this IQueryable<AttendeeCreatorProject> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(aio => (showAllEditions || aio.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProject> FindByKeywords(this IQueryable<AttendeeCreatorProject> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<AttendeeCreatorProject>(false);
                var innerCreatorProjecTitleWhere = PredicateBuilder.New<AttendeeCreatorProject>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerCreatorProjecTitleWhere = innerCreatorProjecTitleWhere.Or(acp => acp.CreatorProject.Title.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerCreatorProjecTitleWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Finds the by is evaluated.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProject> FindByIsEvaluated(this IQueryable<AttendeeCreatorProject> query)
        {
            query = query.Where(aio => aio.Grade != null);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProject> Order(this IQueryable<AttendeeCreatorProject> query)
        {
            query = query.OrderBy(mp => mp.CreateDate);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCreatorProjectDto> Order(this IQueryable<AttendeeCreatorProjectDto> query)
        {
            query = query.OrderBy(dto => dto.CreateDate);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeCreatorProjectRepository</summary>
    public class AttendeeCreatorProjectRepository : Repository<PlataformaRio2CContext, AttendeeCreatorProject>, IAttendeeCreatorProjectRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>Initializes a new instance of the <see cref="AttendeeCreatorProjectRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeCreatorProjectRepository(
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
        private IQueryable<AttendeeCreatorProject> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all attendee creator projects asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <returns></returns>
        private async Task<List<AttendeeCreatorProject>> FindAllAttendeeCreatorProjectsAsync(
            int editionId,
            string searchKeywords)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(searchKeywords);

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all attendee creator project dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <returns></returns>
        private async Task<List<AttendeeCreatorProjectDto>> FindAllAttendeeCreatorProjectDtosAsync(
                int editionId,
                string searchKeywords)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId)
                               .FindByKeywords(searchKeywords)
                               .Select(acp => new AttendeeCreatorProjectDto
                               {
                                   Id = acp.Id,
                                   Uid = acp.Uid,
                                   Grade = acp.Grade,
                                   CreateDate = acp.CreateDate,
                                   CreatorProjectDto = new CreatorProjectDto
                                   {
                                       Uid = acp.CreatorProject.Uid,
                                       Title = acp.CreatorProject.Title,
                                       Logline = acp.CreatorProject.Logline,
                                       InterestDtos = acp.CreatorProject.CreatorProjectInterests.Select(cpi => new InterestDto
                                       {
                                           InterestName = cpi.Interest.Name,
                                           InterestGroupUid = cpi.Interest.InterestGroup.Uid,
                                           InterestGroupName = cpi.Interest.InterestGroup.Name
                                       })
                                   },
                               });

            return await query
                            .Order()
                            .ToListAsync();
        }

        #endregion

        /// <summary>
        /// Finds all dtos paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<AttendeeCreatorProjectDto>> FindAllDtosPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            var attendeeCreatorProjectsDtos = await this.FindAllAttendeeCreatorProjectDtosAsync(editionId, searchKeywords);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeCreatorProjectDto> attendeeCreatorProjectDtosResult = attendeeCreatorProjectsDtos;
            if (editionDto.IsCreatorProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCreatorProjectDtosResult = new List<AttendeeCreatorProjectDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCreatorProjectDtosResult = new List<AttendeeCreatorProjectDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedAttendeeCreatorProjectsIds = await this.FindAllApprovedAttendeeCreatorProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCreatorProjectDtosResult = attendeeCreatorProjectsDtos.Where(dto => approvedAttendeeCreatorProjectsIds.Contains(dto.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCreatorProjectDtosResult = attendeeCreatorProjectsDtos.Where(dto => !approvedAttendeeCreatorProjectsIds.Contains(dto.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeCreatorProjectDtosResult = new List<AttendeeCreatorProjectDto>();
                }

                #endregion
            }

            return await attendeeCreatorProjectDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all creator projects ids paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllCreatorProjectsIdsPagedAsync(
            int editionId,
            string searchKeywords,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            var attendeeCreatorProjects = await this.FindAllAttendeeCreatorProjectsAsync(editionId, searchKeywords);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeCreatorProject> attendeeCreatorProjectResult = attendeeCreatorProjects;
            if (editionDto.IsCreatorProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCreatorProjectResult = new List<AttendeeCreatorProject>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCreatorProjectResult = new List<AttendeeCreatorProject>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedAttendeeCreatorProjectsIds = await this.FindAllApprovedAttendeeCreatorProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCreatorProjectResult = attendeeCreatorProjects.Where(aio => approvedAttendeeCreatorProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCreatorProjectResult = attendeeCreatorProjects.Where(aio => !approvedAttendeeCreatorProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeCreatorProjectResult = new List<AttendeeCreatorProject>();
                }

                #endregion
            }

            var attendeeCreatorProjectsPagedList = await attendeeCreatorProjectResult.ToPagedListAsync(page, pageSize);
            return attendeeCreatorProjectsPagedList
                            .Select(aio => aio.Id)
                            .OrderBy(aioId => aioId)
                            .ToArray();
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectIds">The attendee creator project ids.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindByIdAsync(int attendeeCreatorProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { attendeeCreatorProjectIds })
                            .Select(acp => new AttendeeCreatorProjectDto
                            {

                            });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectUid">The attendee creator project uid.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindByUidAsync(Guid attendeeCreatorProjectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { attendeeCreatorProjectUid })
                            .Select(acp => new AttendeeCreatorProjectDto
                            {

                            });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all by ids asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectIds">The attendee creator project ids.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCreatorProjectDto>> FindAllByIdsAsync(List<int?> attendeeCreatorProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(attendeeCreatorProjectIds)
                            .Select(acp => new AttendeeCreatorProjectDto
                            {

                            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by uids asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectUids">The attendee creator project uids.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCreatorProjectDto>> FindAllByUidsAsync(List<Guid?> attendeeCreatorProjectUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(attendeeCreatorProjectUids)
                            .Select(acp => new AttendeeCreatorProjectDto
                            {

                            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the by title and edition identifier asynchronous.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindByTitleAndEditionIdAsync(string document, int editionId)
        {
            var query = this.GetBaseQuery()
                           .FindByTitleAndEditionId(document, editionId)
                           .Select(acp => new AttendeeCreatorProjectDto
                           {

                           });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all approved attendee creator projects ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllApprovedAttendeeCreatorProjectsIdsAsync(int editionId)
        {
            var edition = await this.editioRepo.FindByIdAsync(editionId);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByIsEvaluated();

            return await query
                            .OrderByDescending(aio => aio.Grade)
                            .Take(edition.CreatorCommissionMaximumApprovedProjectsCount)
                            .Select(aio => aio.Id)
                            .ToArrayAsync();
        }

        /// <summary>
        /// Finds all by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCreatorProject>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId);

            return await query
                            .ToListAsync();
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectId">The attendee creator project identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindDtoToEvaluateAsync(int attendeeCreatorProjectId)
        {
            var query = this.GetBaseQuery()
                               .FindByIds(new List<int?> { attendeeCreatorProjectId })
                               .Select(acp => new AttendeeCreatorProjectDto
                               {
                                   Id = acp.Id,
                                   Uid = acp.Uid,
                                   CreatorProjectDto = new CreatorProjectDto 
                                   {
                                       Title = acp.CreatorProject.Title
                                   }
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectUid">The attendee creator project uid.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindDtoToEvaluateAsync(Guid attendeeCreatorProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(new List<Guid?> { attendeeCreatorProjectUid })
                                .Select(acp => new AttendeeCreatorProjectDto
                                {

                                });

            return await query
                           .FirstOrDefaultAsync();
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
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int> CountPagedAsync(int editionId, string searchKeywords, Guid? evaluationStatusUid, int page, int pageSize)
        {
            var attendeeCreatorProjects = await this.FindAllAttendeeCreatorProjectsAsync(editionId, searchKeywords);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedAttendeeCreatorProjectsIds = await this.FindAllApprovedAttendeeCreatorProjectsIdsAsync(editionId);

            IEnumerable<AttendeeCreatorProject> attendeeCreatorProjectsResult = attendeeCreatorProjects;
            if (editionDto.IsCreatorProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCreatorProjectsResult = new List<AttendeeCreatorProject>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCreatorProjectsResult = new List<AttendeeCreatorProject>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCreatorProjectsResult = attendeeCreatorProjects.Where(aio => approvedAttendeeCreatorProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCreatorProjectsResult = attendeeCreatorProjects.Where(aio => !approvedAttendeeCreatorProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeCreatorProjectsResult = new List<AttendeeCreatorProject>();
                }

                #endregion
            }

            var attendeeCreatorProjectsPagedList = await attendeeCreatorProjectsResult
                                                            .ToPagedListAsync(page, pageSize);

            return attendeeCreatorProjectsPagedList.Count;
        }

        #region Widgets

        /// <summary>
        /// Finds the main information widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectUid">The attendee creator project uid.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindMainInformationWidgetDtoAsync(Guid attendeeCreatorProjectUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUids(new List<Guid?> { attendeeCreatorProjectUid })
                               .Select(acp => new AttendeeCreatorProjectDto
                               {
                                   Id = acp.Id,
                                   Uid = acp.Uid,
                                   Grade = acp.Grade,
                                   CreatorProjectDto = new CreatorProjectDto
                                   {
                                       Title = acp.CreatorProject.Title,
                                       Logline = acp.CreatorProject.Logline,
                                       Name = acp.CreatorProject.Name,
                                       Document = acp.CreatorProject.Document,
                                       AgentName = acp.CreatorProject.AgentName,
                                       Email = acp.CreatorProject.Email,
                                       Curriculum = acp.CreatorProject.Curriculum,
                                       InterestDtos = acp.CreatorProject.CreatorProjectInterests.Select(cpi => new InterestDto
                                       {
                                           InterestName = cpi.Interest.Name,
                                           InterestGroupUid = cpi.Interest.InterestGroup.Uid,
                                       })
                                   },
                                   AttendeeCreatorProjectEvaluationDtos = acp.AttendeeCreatorProjectEvaluations.Select(acpe => new AttendeeCreatorProjectEvaluationDto
                                   {
                                       Grade = acpe.Grade,
                                       EvaluatorUserDto = new UserDto 
                                       {
                                           Id = acpe.EvaluatorUser.Id,
                                       }
                                   })
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the project information widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectUid">The attendee creator project uid.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindProjectInformationWidgetDtoAsync(Guid attendeeCreatorProjectUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUids(new List<Guid?> { attendeeCreatorProjectUid })
                               .Select(acp => new AttendeeCreatorProjectDto
                               {
                                   Id = acp.Id,
                                   Uid = acp.Uid,
                                   CreatorProjectDto = new CreatorProjectDto
                                   {
                                       Title = acp.CreatorProject.Title,
                                       Logline = acp.CreatorProject.Logline,
                                       Description = acp.CreatorProject.Description,
                                       MotivationToDevelop = acp.CreatorProject.MotivationToDevelop,
                                       MotivationToTransform = acp.CreatorProject.MotivationToTransform,
                                       DiversityAndInclusionElements = acp.CreatorProject.DiversityAndInclusionElements,
                                       MarketingStrategy = acp.CreatorProject.MarketingStrategy,
                                       SimilarAudiovisualProjects = acp.CreatorProject.SimilarAudiovisualProjects,
                                       OnlinePlatformsWhereProjectIsAvailable = acp.CreatorProject.OnlinePlatformsWhereProjectIsAvailable,
                                       OnlinePlatformsAudienceReach = acp.CreatorProject.OnlinePlatformsAudienceReach,
                                       ProjectAwards = acp.CreatorProject.ProjectAwards,
                                       ProjectPublicNotice = acp.CreatorProject.ProjectPublicNotice,
                                       PreviouslyDevelopedProjects = acp.CreatorProject.PreviouslyDevelopedProjects,
                                       AssociatedInstitutions = acp.CreatorProject.AssociatedInstitutions,
                                       Links = acp.CreatorProject.Links
                                   }
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the attachments widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCreatorProjectUid">The attendee creator project uid.</param>
        /// <returns></returns>
        public async Task<AttendeeCreatorProjectDto> FindAttachmentsWidgetDtoAsync(Guid attendeeCreatorProjectUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUids(new List<Guid?> { attendeeCreatorProjectUid })
                               .Select(acp => new AttendeeCreatorProjectDto
                               {
                                   Id = acp.Id,
                                   Uid = acp.Uid,
                                   CreatorProjectDto = new CreatorProjectDto
                                   {
                                       Id = acp.CreatorProject.Id,
                                       Uid = acp.CreatorProject.Uid,
                                       ArticleFileUploadDate = acp.CreatorProject.ArticleFileUploadDate,
                                       ArticleFileExtension = acp.CreatorProject.ArticleFileExtension,
                                       ClippingFileUploadDate = acp.CreatorProject.ClippingFileUploadDate,
                                       ClippingFileExtension = acp.CreatorProject.ClippingFileExtension,
                                       OtherFileUploadDate = acp.CreatorProject.OtherFileUploadDate,
                                       OtherFileExtension = acp.CreatorProject.OtherFileExtension,
                                       OtherFileDescription = acp.CreatorProject.OtherFileDescription
                                   }
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        #endregion
    }
}

