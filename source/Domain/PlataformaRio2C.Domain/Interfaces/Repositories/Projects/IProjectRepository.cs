﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-30-2024
// ***********************************************************************
// <copyright file="IProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IProjectRepository</summary>
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project> FindByIdAsync(int projectId);
        Task<Project> FindByUidAsync(Guid projectUid);
        Task<List<ProjectDto>> FindAllDtosToSellAsync(Guid attendeeOrganizationUid, bool showAll, List<int> projectModalityIds);
        Task<IPagedList<ProjectDto>> FindAllDtosToEvaluateAsync(Guid attendeeCollaboratorUid, string searchKeywords, Guid? interestUid, Guid? evaluationStatusUid, int page, int pageSize);
        Task<IPagedList<ProjectBaseDto>> FindAllByDataTableAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, Guid? projectModalityUid, Guid? interestUid, Guid? evaluationStatusUid, string languageCode, int editionId);
        Task<IPagedList<ProjectBaseDto>> FindAllEvaluatorsReportByDataTableAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, Guid? projectModalityUid, Guid? interestUid, Guid? evaluationStatusUid, string languageCode, int editionId);
        Task<IPagedList<ProjectBaseDto>> FindAllProjectsReportByDataTableAsync(int page, int pageSize, List<Tuple<string, string>> sortColumns, string keywords, Guid? projectModalityUid, Guid? interestUid, Guid? evaluationStatusUid, string languageCode, int editionId);
        Task<IPagedList<ProjectDto>> FindAllDtosPagedAsync(int editionId, string searchKeywords, List<Guid?> subgenreInterestUids, Guid? segmentInterestUid, Guid? evaluationStatusUid, bool? showPitchings, int page, int pageSize);
        Task<List<ProjectDto>> FindAllDtosByFiltersAsync(string keywords, bool? showPitchings, List<Guid?> projectModalityUids, List<Guid?> interestUids, List<Guid> projectUids, string languageCode, int editionId);
        Task<List<Project>> FindAllByEditionIdAsync(int editionId);
        Task<int> CountAllByDataTable(int editionId, bool showAllEditions = false);
        Task<int[]> FindAllProjectsIdsPagedAsync(int editionId, string searchKeywords, List<Guid?> subgenreInterestUids, Guid? segmentInterestUid, Guid? evaluationStatusUid, bool? showPitchings, List<Guid?> projectModalityUids, int page, int pageSize);
        Task<int[]> FindAllApprovedCommissionProjectsIdsAsync(int editionId);
        Task<int[]> FindAllApprovedCommissionPitchingProjectsIdsAsync(int editionId);
        Task<int> CountPagedAsync(int editionId, string searchKeywords, List<Guid?> interestUids, Guid? evaluationStatusUid, bool? showPitchings, List<Guid?> projectModalityUids, int page, int pageSize);

        #region Admin Widgets

        Task<ProjectDto> FindAdminDetailsDtoByProjectUidAndByEditionIdAsync(Guid projectUid, int editionId);
        Task<ProjectDto> FindAdminDetailsDtoByProjectIdAndByEditionIdAsync(int projectId, int editionId);
        Task<ProjectDto> FindAdminMainInformationWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindAdminInterestWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindAdminLinksWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindAdminBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindAudiovisualCommissionEvaluationWidgetDtoAsync(Guid projectUid, int userId);
        Task<ProjectDto> FindAudiovisualCommissionEvaluatorsWidgetDtoAsync(Guid projectUid);

        #endregion

        #region Site Widgets

        Task<ProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid, int editionId);
        Task<ProjectDto> FindSiteMainInformationWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteLinksWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteDuplicateDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerEvaluationWidgetDtoByProjectUidAsync(Guid projectUid, Guid attendeeCollaboratorUid);
        Task<ProjectDto> FindDtoToEvaluateAsync(Guid attendeeCollaboratorUid, Guid projectUid);
        Task<ProjectDto> FindDtoToEvaluateAsync(int projectId);

        IEnumerable<AudiovisualProjectSubmissionDto> FindAudiovisualProjectSubmissionDtosByFilter(string keywords, List<Guid?> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, bool showAllEditions = false);
        Task<IPagedList<AudiovisualProjectSubmissionDto>> FindAudiovisualProjectSubmissionDtosByFilterAndByPageAsync(string keywords, List<Guid?> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, int page, int pageSize, bool showAllEditions = false);
        Task<List<AudiovisualProjectSubmissionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAsync(string keywords, List<Guid?> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, bool showAllEditions = false);
        Task<int> CountProjectsByEditionId(int editionId, bool showAllEditions = false);

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