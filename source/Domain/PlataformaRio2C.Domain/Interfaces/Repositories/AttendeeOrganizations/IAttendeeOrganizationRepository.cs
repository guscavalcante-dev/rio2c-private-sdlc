// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-06-2023
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
        Task<int> CountAllByActiveMusicBusinessRoundBuyerNegotiationsAndByDataTable(bool showAllEditions, int? editionId, OrganizationType organizationType);
        Task<AttendeeOrganization> FindByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<IEnumerable<AttendeeOrganizationBaseDto>> FindOrganizationBaseDtosByCollaboratorIdAndEditionIdAsync(int collaboratorId, int editionId, bool showAllEditions = false);
        Task<AttendeeOrganizationDto> FindDtoByAttendeeOrganizationUid(Guid attendeeOrganizationUid);
        Task<IPagedList<NegotiationAttendeeOrganizationBaseDto>> FindAllByActiveMusicBusinessRoundBuyerNegotiationsAndByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, int editionId, int languageId);

        #region Common Widgets

        Task<AttendeeOrganizationSiteDetailsDto> FindDetailsDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteDetailsDto> FindDetailsDtoByOrganizationUidAndByOrganizationTypeUidAsync(Guid organizationUid, Guid organizationTypeUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteAddressWidgetDto> FindAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteActivityWidgetDto> FindActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteTargetAudienceWidgetDto> FindTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationSiteInterestWidgetDto> FindInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId, bool showAllEditions);
        Task<AttendeeOrganizationApiConfigurationWidgetDto> FindApiConfigurationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);

        #endregion

        #region Admin Widgets

        Task<AttendeeOrganizationMainInformationWidgetDto> FindAdminMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, Guid organizationTypeUid, int editionId);
        Task<AttendeeOrganizationExecutiveWidgetDto> FindAdminExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, Guid organizationTypeUid, Guid collaboratorTypeUid, int editionId);

        #endregion

        #region Site Widgets

        Task<AttendeeOrganizationMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);
        Task<AttendeeOrganizationExecutiveWidgetDto> FindSiteExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId);

        #endregion

        #region Site Projects Widgets

        Task<List<AttendeeOrganizationDto>> FindAllDtoByBuyerProjectUid(Guid projectUid);
        Task<IPagedList<MatchAttendeeOrganizationDto>> FindAllAudiovisualMatchingAttendeeOrganizationsDtosAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize);
        Task<IPagedList<MatchAttendeeOrganizationDto>> FindAllMusicMatchingAttendeeOrganizationsDtosAsync(int editionId, MusicBusinessRoundProjectDto projectDto, string searchKeywords, int page, int pageSize);
        Task<IPagedList<AttendeeOrganizationDto>> FindAllDtoByProjectBuyerAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize);
        Task<IPagedList<AttendeeOrganizationDto>> FindAllMusicBusinessRoundProjectBuyerAsync(int editionId, MusicBusinessRoundProjectDto projectDto, string searchKeywords, int page, int pageSize);

        #endregion

        #region Negotiations

        #region Players
        Task<NegotiationAttendeeOrganizationBaseDto> FindAllBaseDtoByUid(Guid attendeeOrganizationUid, int languageId);
        Task<IPagedList<NegotiationAttendeeOrganizationBaseDto>> FindAllByActiveBuyerNegotiationsAndByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, int editionId, int languageId, OrganizationType organizationType);
        Task<List<NegotiationAttendeeOrganizationBaseDto>> FindAllBaseDtoByActiveBuyerNegotiations(string keywords, List<Guid> selectedAttendeeOrganizationsUids, int editionId, int languageId, OrganizationType organizationType);
        Task<int> CountAllByActiveBuyerNegotiationsAndByDataTable(bool showAllEditions, int? editionId, OrganizationType organizationType);
        #endregion

        #region Producers

        Task<IPagedList<NegotiationAttendeeOrganizationBaseDto>> FindAllByActiveSellerNegotiationsAndByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, int editionId, int languageId);
        Task<List<NegotiationAttendeeOrganizationBaseDto>> FindAllBaseDtoByActiveSellerNegotiations(string keywords, List<Guid> selectedAttendeeOrganizationsUids, int editionId, int languageId);
        Task<int> CountAllByActiveSellerNegotiationsAndByDataTable(bool showAllEditions, int? editionId);

        #endregion

        #endregion

        #region Api

        Task<List<AttendeeOrganizationApiConfigurationWidgetDto>> FindAllApiConfigurationWidgetDtoByHighlight(int editionId, Guid organizationTypeUid);

        #endregion
    }
}