// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 01/16/2020
// ***********************************************************************
// <copyright file="IProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IProjectRepository</summary>
    public interface IProjectRepository : IRepository<Project>
    {
        #region Site Widgets

        Task<List<ProjectDto>> FindAllDtosToSellAsync(Guid attendeeOrganizationUid, bool showAll);
        Task<IPagedList<ProjectDto>> FindAllDtosToEvaluateAsync(Guid attendeeCollaboratorUid, string searchKeywords, Guid? interestUid, Guid? evaluationStatusUid, int page, int pageSize);
        Task<IPagedList<ProjectBaseDto>> FindAllPitchingBaseDtosByFiltersAndByPageAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, Guid? interestUid, string languageCode, int editionId);
        Task<List<ProjectDto>> FindAllPitchingDtosByFiltersAsync(string keywords, Guid? interestUid, List<Guid> projectUids, string languageCode, int editionId);
        Task<ProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteMainInformationWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteLinksWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteDuplicateDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerEvaluationWidgetDtoByProjectUidAsync(Guid projectUid, Guid attendeeCollaboratorUid);
        Task<int> CountAllByDataTable(int editionId, bool showAllEditions = false);
        IEnumerable<AudiovisualProjectSubscriptionDto> FindAudiovisualSubscribedProjectsDtosByFilter(string keywords, Guid? interestUid, int editionId, bool isPitching, Guid? targetAudienceUid, DateTime? startDate, DateTime? endDate, bool showAllEditions = false);
        Task<IPagedList<AudiovisualProjectSubscriptionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAndByPageAsync(string keywords, Guid? interestUid, int editionId, bool isPitching, Guid? targetAudienceUid, DateTime? startDate, DateTime? endDate, int page, int pageSize, bool showAllEditions = false);
        Task<List<AudiovisualProjectSubscriptionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAsync(string keywords, Guid? interestUid, int editionId, bool isPitching, Guid? targetAudienceUid, DateTime? startDate, DateTime? endDate, bool showAllEditions = false);
        #endregion

        #region Old methods

        IEnumerable<Project> GetAllByAdmin();
        IEnumerable<Project> GetAllExcel();
        IEnumerable<Project> GetDataExcel();

        Project GetSimpleWithProducer(Expression<Func<Project, bool>> filter);
        Project GetSimpleWithPlayers(Expression<Func<Project, bool>> filter);

        Project GetWithPlayerSelection(Guid uid);

        IQueryable<Project> GetAllOption(Expression<Func<Project, bool>> filter);

        int CountUnsent();
        int CountSent();

        #endregion
    }
}