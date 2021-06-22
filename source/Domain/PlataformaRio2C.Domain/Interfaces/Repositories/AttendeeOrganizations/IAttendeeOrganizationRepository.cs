// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-22-2021
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
        Task<List<AttendeeOrganizationBaseDto>> FindAllBaseDtosByEditionUidAsync(int editionId, bool showAllEditions, Guid organizationTypeUid);
        Task<List<AttendeeOrganization>> FindAllByUidsAsync(List<Guid> attendeeOrganizationsUids);
        Task<AttendeeOrganization> FindByUidAsync(Guid attendeeOrganizationUid);

        #region Common Widgets

        Task<AttendeeOrganizationSiteDetailsDto> FindDetailsDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteExecutiveWidgetDto> FindExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteAddressWidgetDto> FindAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteActivityWidgetDto> FindActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteTargetAudienceWidgetDto> FindTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteInterestWidgetDto> FindInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);

        #endregion

        #region Admin Widgets

        Task<AttendeeOrganizationMainInformationWidgetDto> FindAdminMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, Guid organizationTypeUid, int editionId);

        #endregion

        #region Site Widgets

        Task<AttendeeOrganizationMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);

        #endregion

        #region Site Projects Widgets

        Task<List<AttendeeOrganizationDto>> FindAllDtoByBuyerProjectUid(Guid projectUid);
        Task<IPagedList<MatchAttendeeOrganizationDto>> FindAllDtoByMatchingProjectBuyerAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize);
        Task<IPagedList<AttendeeOrganizationDto>> FindAllDtoByProjectBuyerAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize);

        #endregion
    }
}