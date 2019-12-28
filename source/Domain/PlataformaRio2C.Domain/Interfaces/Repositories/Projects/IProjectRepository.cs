// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-28-2019
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
        Task<IPagedList<ProjectBaseDto>> FindAllPitchingProjectsDtoAsync(string searchKeywords, string languageCode, int page, int pageSize);
        Task<ProjectDto> FindPitchingProjectDtoByUidAsync(Guid projectUid);
        Task<List<ProjectBaseDto>> FindAllPitchingProjectsDtoByKeywordsAsync(string searchKeywords, string languageCode);
        Task<ProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteMainInformationWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteLinksWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteDuplicateDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerEvaluationWidgetDtoByProjectUidAsync(Guid projectUid, Guid attendeeCollaboratorUid);

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