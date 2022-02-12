// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectRepository.cs" company="Softo">
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
    #region AttendeeCartoonProject IQueryable Extensions

    /// <summary>
    /// AttendeeCartoonProjectIQueryableExtensions
    /// </summary>
    internal static class AttendeeCartoonProjectIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCartoonProjectIds">The cartoon projects ids.</param>
        /// <returns>IQueryable&lt;AttendeeCartoonProject&gt;.</returns>
        internal static IQueryable<AttendeeCartoonProject> FindByIds(this IQueryable<AttendeeCartoonProject> query, List<int?> attendeeCartoonProjectIds)
        {
            if (attendeeCartoonProjectIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => attendeeCartoonProjectIds.Contains(aio.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCartoonProjectUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByUids(this IQueryable<AttendeeCartoonProject> query, List<Guid?> attendeeCartoonProjectUids)
        {
            if (attendeeCartoonProjectUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => attendeeCartoonProjectUids.Contains(aio.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> IsNotDeleted(this IQueryable<AttendeeCartoonProject> query)
        {
            query = query.Where(acp => !acp.CartoonProject.IsDeleted && !acp.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by document.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="title">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>IQueryable&lt;AttendeeCartoonProject&gt;.</returns>
        internal static IQueryable<AttendeeCartoonProject> FindByTitleAndEditionId(this IQueryable<AttendeeCartoonProject> query, string title, int editionId)
        {
            query = query.Where(acp => acp.CartoonProject.Title == title
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
        internal static IQueryable<AttendeeCartoonProject> FindByEditionId(this IQueryable<AttendeeCartoonProject> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(aio => (showAllEditions || aio.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByKeywords(this IQueryable<AttendeeCartoonProject> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<AttendeeCartoonProject>(false);
                var innerCartoonProjecTitleWhere = PredicateBuilder.New<AttendeeCartoonProject>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerCartoonProjecTitleWhere = innerCartoonProjecTitleWhere.Or(acp => acp.CartoonProject.Title.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerCartoonProjecTitleWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Finds the by is evaluated.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByIsEvaluated(this IQueryable<AttendeeCartoonProject> query)
        {
            query = query.Where(aio => aio.Grade != null);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> Order(this IQueryable<AttendeeCartoonProject> query)
        {
            query = query.OrderBy(mp => mp.CreateDate);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProjectDto> Order(this IQueryable<AttendeeCartoonProjectDto> query)
        {
            query = query.OrderBy(aioDto => aioDto.AttendeeCartoonProject.CreateDate);

            return query;
        }

        /// <summary>
        /// Finds the by project format uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="projectFormatUids">The project format uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByProjectFormatUids(this IQueryable<AttendeeCartoonProject> query, List<Guid?> projectFormatUids)
        {
            if (projectFormatUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(acp => projectFormatUids.Any(projectFormatUid => !acp.CartoonProject.CartoonProjectFormat.IsDeleted &&
                                                                                      acp.CartoonProject.CartoonProjectFormat.Uid == projectFormatUid));
            }

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeCartoonProjectRepository</summary>
    public class AttendeeCartoonProjectRepository : Repository<PlataformaRio2CContext, AttendeeCartoonProject>, IAttendeeCartoonProjectRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeCartoonProjectRepository(
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
        private IQueryable<AttendeeCartoonProject> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all attendee cartoon projects asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUids">The project format uids.</param>
        /// <returns></returns>
        private async Task<List<AttendeeCartoonProject>> FindAllAttendeeCartoonProjectsAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> projectFormatUids)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(searchKeywords)
                                .FindByProjectFormatUids(projectFormatUids);

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all json dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUids">The project format uids.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        private async Task<List<AttendeeCartoonProjectJsonDto>> FindAllJsonDtosAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> projectFormatUids,
            List<Tuple<string, string>> sortColumns)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId, false)
                               .FindByKeywords(searchKeywords)
                               .FindByProjectFormatUids(projectFormatUids)
                               .DynamicOrder<AttendeeCartoonProject>(
                                   sortColumns,
                                   null,
                                   new List<string> { "CreateDate", "UpdateDate" },
                                   "CreateDate")
                               .Select(acp => new AttendeeCartoonProjectJsonDto
                               {
                                   AttendeeCartoonProjectId = acp.Id,
                                   AttendeeCartoonProjectUid = acp.Uid,
                                   CartoonProjectId = acp.CartoonProject.Id,
                                   CartoonProjectUid = acp.CartoonProject.Uid,
                                   CartoonProjectTitle = acp.CartoonProject.Title,
                                   CartoonProjectFormatName = acp.CartoonProject.CartoonProjectFormat.Name,
                                   //ImageUploadDate = acp.CartoonProject.ImageUploadDate,
                                   Grade = acp.Grade,
                                   EvaluationsCount = acp.AttendeeCartoonProjectEvaluations.Count(aioe => !aioe.IsDeleted),
                                   CreateDate = acp.CreateDate,
                                   UpdateDate = acp.UpdateDate
                               });

            return await query
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all attendee cartoon project dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <returns></returns>
        private async Task<List<AttendeeCartoonProjectDto>> FindAllAttendeeCartoonProjectDtosAsync(
                int editionId,
                string searchKeywords)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId)
                               .FindByKeywords(searchKeywords)
                               .Select(aio => new AttendeeCartoonProjectDto
                               {
                                   AttendeeCartoonProject = aio,
                                   CartoonProject = aio.CartoonProject,
                                   AttendeeCartoonProjectCollaboratorDtos = aio.AttendeeCartoonProjectCollaborators
                                                                                       .Where(aioc => !aioc.IsDeleted)
                                                                                       .Select(aioc =>
                                                                                       new AttendeeCartoonProjectCollaboratorDto
                                                                                       {
                                                                                           AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                           Collaborator = aioc.AttendeeCollaborator.Collaborator
                                                                                       }),
                                   AttendeeCartoonProjectEvaluationDtos = aio.AttendeeCartoonProjectEvaluations
                                                                                      .Where(aioe => !aioe.IsDeleted)
                                                                                      .Select(aioe => new AttendeeCartoonProjectEvaluationDto
                                                                                      {
                                                                                          AttendeeCartoonProjectEvaluation = aioe,
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
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        private async Task<List<AttendeeCartoonProjectJsonDto>> FindAllJsonDtosAsync(
            int editionId,
            string searchKeywords,
            List<Tuple<string, string>> sortColumns)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId, false)
                               .FindByKeywords(searchKeywords)
                               .DynamicOrder<AttendeeCartoonProject>(
                                   sortColumns,
                                   null,
                                   new List<string> { "CreateDate", "UpdateDate" },
                                   "CreateDate")
                               .Select(aio => new AttendeeCartoonProjectJsonDto
                               {
                                   AttendeeCartoonProjectId = aio.Id,
                                   AttendeeCartoonProjectUid = aio.Uid,
                                   CartoonProjectId = aio.CartoonProject.Id,
                                   CartoonProjectUid = aio.CartoonProject.Uid,
                                   CartoonProjectTitle = aio.CartoonProject.Title,
                                   CartoonProjectFormatName = aio.CartoonProject.CartoonProjectFormat.Name,
                                   Grade = aio.Grade,
                                   EvaluationsCount = aio.AttendeeCartoonProjectEvaluations.Count(aioe => !aioe.IsDeleted),

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
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IPagedList<AttendeeCartoonProjectDto>> FindAllDtosPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> projectFormatUids,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            var attendeeCartoonProjectsDtos = await this.FindAllAttendeeCartoonProjectDtosAsync(editionId, searchKeywords);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeCartoonProjectDto> attendeeCartoonProjectDtosResult = attendeeCartoonProjectsDtos;
            if (editionDto.IsCartoonProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectDtosResult = new List<AttendeeCartoonProjectDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectDtosResult = new List<AttendeeCartoonProjectDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedAttendeeCartoonProjectsIds = await this.FindAllApprovedAttendeeCartoonProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectDtosResult = attendeeCartoonProjectsDtos.Where(aioDto => approvedAttendeeCartoonProjectsIds.Contains(aioDto.AttendeeCartoonProject.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectDtosResult = attendeeCartoonProjectsDtos.Where(aioDto => !approvedAttendeeCartoonProjectsIds.Contains(aioDto.AttendeeCartoonProject.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeCartoonProjectDtosResult = new List<AttendeeCartoonProjectDto>();
                }

                #endregion
            }

            return await attendeeCartoonProjectDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all json dtos paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUids">The project format uids.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        public async Task<IPagedList<AttendeeCartoonProjectJsonDto>> FindAllJsonDtosPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> projectFormatUids,
            Guid? evaluationStatusUid,
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns)
        {
            var attendeeCartoonProjectJsonDtos = await this.FindAllJsonDtosAsync(editionId, searchKeywords, projectFormatUids, sortColumns);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeCartoonProjectJsonDto> attendeeCartoonProjectJsonDtosResult = attendeeCartoonProjectJsonDtos;
            if (editionDto.IsCartoonProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectJsonDtosResult = new List<AttendeeCartoonProjectJsonDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectJsonDtosResult = new List<AttendeeCartoonProjectJsonDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedCartoonProjectsIds = await this.FindAllApprovedAttendeeCartoonProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectJsonDtosResult = attendeeCartoonProjectJsonDtos.Where(w => approvedCartoonProjectsIds.Contains(w.AttendeeCartoonProjectId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectJsonDtosResult = attendeeCartoonProjectJsonDtos.Where(w => !approvedCartoonProjectsIds.Contains(w.AttendeeCartoonProjectId));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeCartoonProjectJsonDtosResult = new List<AttendeeCartoonProjectJsonDto>();
                }

                #endregion
            }

            return await attendeeCartoonProjectJsonDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all cartoon projects ids paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="projectFormatUids">The project format uids.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllCartoonProjectsIdsPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> projectFormatUids,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            var attendeeCartoonProjects = await this.FindAllAttendeeCartoonProjectsAsync(editionId, searchKeywords, projectFormatUids);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<AttendeeCartoonProject> attendeeCartoonProjectResult = attendeeCartoonProjects;
            if (editionDto.IsCartoonProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectResult = new List<AttendeeCartoonProject>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectResult = new List<AttendeeCartoonProject>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedAttendeeCartoonProjectsIds = await this.FindAllApprovedAttendeeCartoonProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectResult = attendeeCartoonProjects.Where(aio => approvedAttendeeCartoonProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectResult = attendeeCartoonProjects.Where(aio => !approvedAttendeeCartoonProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeCartoonProjectResult = new List<AttendeeCartoonProject>();
                }

                #endregion
            }

            var attendeeCartoonProjectsPagedList = await attendeeCartoonProjectResult.ToPagedListAsync(page, pageSize);
            return attendeeCartoonProjectsPagedList
                            .Select(aio => aio.Id)
                            .OrderBy(aioId => aioId)
                            .ToArray();
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectIds">The attendee cartoon project ids.</param>
        /// <returns></returns>
        public async Task<AttendeeCartoonProject> FindByIdAsync(int attendeeCartoonProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { attendeeCartoonProjectIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The cartoon project uid.</param>
        /// <returns>Task&lt;AttendeeCartoonProject&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeCartoonProject> FindByUidAsync(Guid attendeeCartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { attendeeCartoonProjectUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all by ids asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectIds">The attendee cartoon project ids.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCartoonProject>> FindAllByIdsAsync(List<int?> attendeeCartoonProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(attendeeCartoonProjectIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by uids asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUids">The attendee cartoon project uids.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCartoonProject>> FindAllByUidsAsync(List<Guid?> attendeeCartoonProjectUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(attendeeCartoonProjectUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the by title and edition identifier asynchronous.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCartoonProject> FindByTitleAndEditionIdAsync(string document, int editionId)
        {
            var query = this.GetBaseQuery()
                           .FindByTitleAndEditionId(document, editionId);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all approved attendee cartoon projects ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllApprovedAttendeeCartoonProjectsIdsAsync(int editionId)
        {
            var edition = await this.editioRepo.FindByIdAsync(editionId);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByIsEvaluated();

            return await query
                            .OrderByDescending(aio => aio.Grade)
                            .Take(edition.CartoonCommissionMaximumApprovedProjectsCount)//VOLTAR AQUI
                            .Select(aio => aio.Id)
                            .ToArrayAsync();
        }

        /// <summary>
        /// Finds all by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCartoonProject>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId);

            return await query
                            .ToListAsync();
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectId">The attendee cartoon project identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeCartoonProjectDto> FindDtoToEvaluateAsync(int attendeeCartoonProjectId)
        {
            var query = this.GetBaseQuery()
                               .FindByIds(new List<int?> { attendeeCartoonProjectId })
                               .Select(aio => new AttendeeCartoonProjectDto
                               {
                                   AttendeeCartoonProject = aio,
                                   CartoonProject = aio.CartoonProject,
                                   AttendeeCartoonProjectCollaboratorDtos = aio.AttendeeCartoonProjectCollaborators
                                                                                       .Where(aioc => !aioc.IsDeleted)
                                                                                       .Select(aioc =>
                                                                                       new AttendeeCartoonProjectCollaboratorDto
                                                                                       {
                                                                                           AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                           Collaborator = aioc.AttendeeCollaborator.Collaborator
                                                                                       })
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeCartoonProjectDto> FindDtoToEvaluateAsync(Guid attendeeCartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(new List<Guid?> { attendeeCartoonProjectUid })
                                .Select(aio => new AttendeeCartoonProjectDto
                                {
                                    AttendeeCartoonProject = aio,
                                    CartoonProject = aio.CartoonProject,
                                    AttendeeCartoonProjectCollaboratorDtos = aio.AttendeeCartoonProjectCollaborators
                                                                                        .Where(aioc => !aioc.IsDeleted)
                                                                                        .Select(aioc =>
                                                                                        new AttendeeCartoonProjectCollaboratorDto
                                                                                        {
                                                                                            AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                            Collaborator = aioc.AttendeeCollaborator.Collaborator
                                                                                        })
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
        public async Task<int> CountPagedAsync(int editionId, string searchKeywords, List<Guid?> projectFormatUids, Guid? evaluationStatusUid, int page, int pageSize)
        {
            var attendeeCartoonProjects = await this.FindAllAttendeeCartoonProjectsAsync(editionId, searchKeywords, projectFormatUids);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedAttendeeCartoonProjectsIds = await this.FindAllApprovedAttendeeCartoonProjectsIdsAsync(editionId);

            IEnumerable<AttendeeCartoonProject> attendeeCartoonProjectsResult = attendeeCartoonProjects;
            if (editionDto.IsCartoonProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectsResult = new List<AttendeeCartoonProject>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectsResult = new List<AttendeeCartoonProject>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    attendeeCartoonProjectsResult = attendeeCartoonProjects.Where(aio => approvedAttendeeCartoonProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    attendeeCartoonProjectsResult = attendeeCartoonProjects.Where(aio => !approvedAttendeeCartoonProjectsIds.Contains(aio.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    attendeeCartoonProjectsResult = new List<AttendeeCartoonProject>();
                }

                #endregion
            }

            var attendeeCartoonProjectsPagedList = await attendeeCartoonProjectsResult
                                                 .ToPagedListAsync(page, pageSize);

            return attendeeCartoonProjectsPagedList.Count;
        }

        /// <summary>
        /// Finds the main information widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeCartoonProjectDto> FindMainInformationWidgetDtoAsync(Guid attendeeCartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUids(new List<Guid?> { attendeeCartoonProjectUid })
                               .Select(x => new AttendeeCartoonProjectDto
                               {
                                   AttendeeCartoonProject = x,
                                   CartoonProjectDto = new CartoonProjectDto
                                   {
                                       Id = x.CartoonProject.Id,
                                       Title =  x.CartoonProject.Title,
                                       LogLine = x.CartoonProject.LogLine,
                                       CartoonProjectFormatName = x.CartoonProject.CartoonProjectFormat.Name,
                                       EachEpisodePlayingTime = x.CartoonProject.EachEpisodePlayingTime,
                                       Motivation = x.CartoonProject.Motivation,
                                       NumberOfEpisodes = x.CartoonProject.NumberOfEpisodes.ToString(),
                                       ProductionPlan = x.CartoonProject.ProductionPlan,
                                       ProjectBibleUrl = x.CartoonProject.BibleUrl,
                                       ProjectTeaserUrl = x.CartoonProject.TeaserUrl,
                                       Summary = x.CartoonProject.Summary,
                                       TotalValueOfProject = x.CartoonProject.TotalValueOfProject.ToString()
                                   },
                                   AttendeeCartoonProjectEvaluationDtos = x.AttendeeCartoonProjectEvaluations
                                                                                        .Where(aioe => !aioe.IsDeleted)
                                                                                        .Select(aioe =>
                                                                                          new AttendeeCartoonProjectEvaluationDto
                                                                                          {
                                                                                              AttendeeCartoonProjectEvaluation = aioe,
                                                                                              AttendeeCartoonProject = aioe.AttendeeCartoonProject,
                                                                                              EvaluatorUser = aioe.EvaluatorUser
                                                                                          }),
                                   AttendeeCartoonProjectCollaboratorDtos = x.AttendeeCartoonProjectCollaborators
                                                                                        .Where(aioc => !aioc.IsDeleted)
                                                                                        .Select(aioc =>
                                                                                        new AttendeeCartoonProjectCollaboratorDto
                                                                                        {
                                                                                            AttendeeCollaborator = aioc.AttendeeCollaborator,
                                                                                            Collaborator = aioc.AttendeeCollaborator.Collaborator
                                                                                        })
                               });

            return await query
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the creators widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        public async Task<List<CartoonProjectCreatorDto>> FindCreatorsWidgetDtoAsync(Guid attendeeCartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { attendeeCartoonProjectUid })
                            .SelectMany(acp => acp.CartoonProject.CartoonProjectCreators
                                .Where(cpc => !acp.IsDeleted &&
                                              !cpc.IsDeleted &&
                                              !acp.CartoonProject.IsDeleted)
                                   .Select(cpo => new CartoonProjectCreatorDto
                                   {
                                       FirstName = cpo.FirstName,
                                       LastName = cpo.LastName,
                                       Email = cpo.Email,
                                       CellPhone = cpo.CellPhone,
                                       MiniBio = cpo.MiniBio,
                                       PhoneNumber = cpo.PhoneNumber
                                   }));

            return await query
                            .ToListAsync();
        }

        /// <summary>
        /// Finds the organization widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        public async Task<CartoonProjectOrganizationDto> FindOrganizationWidgetDtoAsync(Guid attendeeCartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                             .FindByUids(new List<Guid?> { attendeeCartoonProjectUid })
                             .SelectMany(acp => acp.CartoonProject.CartoonProjectOrganizations
                                 .Where(cpo => !acp.IsDeleted &&
                                               !cpo.IsDeleted &&
                                               !acp.CartoonProject.IsDeleted)
                                    .Select(cpo => new CartoonProjectOrganizationDto
                                    {
                                        Name = cpo.Name,
                                        TradeName = cpo.TradeName,
                                        Document = cpo.Document,
                                        PhoneNumber = cpo.PhoneNumber,
                                        ReelUrl = cpo.ReelUrl,
                                        Address = cpo.Address.Address1
                                    }));

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the evaluators widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <returns></returns>
        public async Task<AttendeeCartoonProjectDto> FindEvaluatorsWidgetDtoAsync(Guid attendeeCartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                              .FindByUids(new List<Guid?> { attendeeCartoonProjectUid })
                              .Select(aio => new AttendeeCartoonProjectDto
                              {
                                  AttendeeCartoonProject = aio,
                                  CartoonProject = aio.CartoonProject,
                                  AttendeeCartoonProjectEvaluationDtos = aio.AttendeeCartoonProjectEvaluations
                                                                                        .Where(aioe => !aioe.IsDeleted)
                                                                                        .Select(aioe =>
                                                                                          new AttendeeCartoonProjectEvaluationDto
                                                                                          {
                                                                                              AttendeeCartoonProjectEvaluation = aioe,
                                                                                              AttendeeCartoonProject = aioe.AttendeeCartoonProject,
                                                                                              EvaluatorUser = aioe.EvaluatorUser
                                                                                          })
                              });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the evaluation grade widget dto asynchronous.
        /// </summary>
        /// <param name="attendeeCartoonProjectUid">The attendee cartoon project uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCartoonProjectDto> FindEvaluationGradeWidgetDtoAsync(Guid attendeeCartoonProjectUid, int userId)
        {
            var query = this.GetBaseQuery()
                              .FindByUids(new List<Guid?> { attendeeCartoonProjectUid })
                              .Select(aio => new AttendeeCartoonProjectDto
                              {
                                  AttendeeCartoonProject = aio,
                                  CartoonProject = aio.CartoonProject,
                                  AttendeeCartoonProjectEvaluationDtos = aio.AttendeeCartoonProjectEvaluations
                                                                                      .Where(aioe => !aioe.IsDeleted)
                                                                                      .Select(aioe => new AttendeeCartoonProjectEvaluationDto
                                                                                      {
                                                                                          AttendeeCartoonProjectEvaluation = aioe,
                                                                                          EvaluatorUser = aioe.EvaluatorUser
                                                                                      }).ToList(),
                                  //Current AttendeeInnovationOrganizationEvaluation by user Id
                                  AttendeeCartoonProjectEvaluationDto = aio.AttendeeCartoonProjectEvaluations
                                                                                   .Where(aioe => !aioe.IsDeleted && aioe.EvaluatorUserId == userId)
                                                                                   .Select(aioe => new AttendeeCartoonProjectEvaluationDto
                                                                                   {
                                                                                       AttendeeCartoonProjectEvaluation = aioe,
                                                                                       EvaluatorUser = aioe.EvaluatorUser
                                                                                   }).FirstOrDefault()
                              });

            return await query
                            .FirstOrDefaultAsync();
        }
    }
}

