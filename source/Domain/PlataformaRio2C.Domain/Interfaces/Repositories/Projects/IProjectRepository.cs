// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2021
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
        Task<IPagedList<ProjectBaseDto>> FindAllBaseDtosByFiltersAndByPageAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, bool showPitchings, Guid? interestUid, string languageCode, int editionId);
        Task<List<ProjectDto>> FindAllDtosByFiltersAsync(string keywords, bool showPitchings, Guid? interestUid, List<Guid> projectUids, string languageCode, int editionId);
        Task<ProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteMainInformationWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteLinksWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteDuplicateDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerEvaluationWidgetDtoByProjectUidAsync(Guid projectUid, Guid attendeeCollaboratorUid);
        Task<ProjectDto> FindDtoToEvaluateAsync(Guid attendeeCollaboratorUid, Guid projectUid);
        Task<int> CountAllByDataTable(int editionId, bool showAllEditions = false);
        IEnumerable<AudiovisualProjectSubscriptionDto> FindAudiovisualSubscribedProjectsDtosByFilter(string keywords, List<Guid> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, bool showAllEditions = false);
        Task<IPagedList<AudiovisualProjectSubscriptionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAndByPageAsync(string keywords, List<Guid> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, int page, int pageSize, bool showAllEditions = false);
        Task<List<AudiovisualProjectSubscriptionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAsync(string keywords, List<Guid> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, bool showAllEditions = false);
        #endregion

        #region Dropdown

        Task<IPagedList<ProjectDto>> FindAllDropdownDtoPaged(int editionId, string keywords, string customFilter, Guid? buyerOrganizationUid, int page, int pageSize);

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