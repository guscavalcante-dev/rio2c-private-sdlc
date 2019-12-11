// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-09-2019
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
        Task<IPagedList<ProjectDto>> FindAllDtosToEvaluateAsync(Guid attendeeOrganizationUid, string searchKeywords, Guid? interestUid, Guid? evaluationStatusUid, int page, int pageSize);
        Task<ProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteMainInformationWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteLinksWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid);
        Task<ProjectDto> FindSiteDuplicateDtoByProjectUidAsync(Guid projectUid);

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