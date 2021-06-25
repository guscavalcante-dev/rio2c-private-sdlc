// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-25-2021
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
        Task<AttendeeOrganizationSiteAddressWidgetDto> FindAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteActivityWidgetDto> FindActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteTargetAudienceWidgetDto> FindTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteInterestWidgetDto> FindInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);

        #endregion

        #region Admin Widgets

        Task<AttendeeOrganizationMainInformationWidgetDto> FindAdminMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, Guid organizationTypeUid, int editionId);
        Task<AttendeeOrganizationExecutiveWidgetDto> FindAdminExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, Guid organizationTypeUid, int editionId);

        #endregion

        #region Site Widgets

        Task<AttendeeOrganizationMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttendeeOrganizationExecutiveWidgetDto> FindSiteExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);

        #endregion

        #region Site Projects Widgets

        Task<List<AttendeeOrganizationDto>> FindAllDtoByBuyerProjectUid(Guid projectUid);
        Task<IPagedList<MatchAttendeeOrganizationDto>> FindAllDtoByMatchingProjectBuyerAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize);
        Task<IPagedList<AttendeeOrganizationDto>> FindAllDtoByProjectBuyerAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize);

        #endregion

        #region Negotiations

        Task<IPagedList<SellerAttendeeOrganizationBaseDto>> FindAllByActiveSellerNegotiationsAndByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, Guid organizationTypeUid, int editionId, int languageId);
        Task<List<SellerAttendeeOrganizationBaseDto>> FindAllBaseDtoByActiveSellerNegotiations(string keywords, List<Guid> selectedAttendeeOrganizationsUids, Guid organizationTypeUid, int editionId, int languageId);
        Task<int> CountAllByActiveSellerNegotiationsAndByDataTable(Guid organizationTypeUid, bool showAllEditions, int? editionId);

        #endregion
    }
}