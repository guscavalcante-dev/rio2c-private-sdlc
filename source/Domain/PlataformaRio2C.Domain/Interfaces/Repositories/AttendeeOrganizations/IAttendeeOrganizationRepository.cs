// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-17-2019
// ***********************************************************************
// <copyright file="IAttendeeOrganizationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeOrganizationRepository</summary>
    public interface IAttendeeOrganizationRepository : IRepository<AttendeeOrganization>
    {
        Task<List<AttendeeOrganizationBaseDto>> FindAllBaseDtosByEditionUidAsync(int editionId, bool showAllEditions);
        Task<List<AttendeeOrganization>> FindAllByUidsAsync(List<Guid> attendeeOrganizationsUids);

        #region Site Widgets

        Task<AttemdeeOrganizationSiteDetailsDto> FindSiteDetailstDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttemdeeOrganizationSiteMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttendeeOrganizationSiteExecutiveWidgetDto> FindSiteExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttendeeOrganizationSiteAddressWidgetDto> FindSiteAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttendeeOrganizationSiteActivityWidgetDto> FindSiteActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttendeeOrganizationSiteTargetAudienceWidgetDto> FindSiteTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttendeeOrganizationSiteInterestWidgetDto> FindSiteInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);

        #endregion

        #region Site Projects Widgets

        Task<List<AttendeeOrganizationDto>> FindAllDtoByBuyerProjectUid(Guid projectUid);
        Task<IPagedList<MatchAttendeeOrganizationDto>> FindAllDtoByMatchingProjectBuyerAsync(int editionId, ProjectDto projectDto, int page, int pageSize);
        Task<IPagedList<AttendeeOrganizationDto>> FindAllDtoByProjectBuyerAsync(int editionId, ProjectDto projectDto, int page, int pageSize);

        #endregion
    }
}