﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-25-2025
// ***********************************************************************
// <copyright file="IAttendeeCollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeCollaboratorRepository</summary>
    public interface IAttendeeCollaboratorRepository : IRepository<AttendeeCollaborator>
    {
        #region Widgets
        Task<AttendeeCollaboratorSiteDetailsDto> FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorSiteDetailsDto> FindSiteDetailstDtoByCollaboratorUidAndByCollaboratorTypeUidAsync(Guid collaboratorUid, Guid collaboratorTypeUid);
        Task<AttendeeCollaboratorSiteMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorSiteCompanyWidgetDto> FindSiteCompanyWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorOnboardingInfoWidgetDto> FindOnboardingInfoWidgetDtoAsync(Guid collaboratorUid, Guid collaboratorTypeUid, int editionId);
        Task<AttendeeCollaboratorTracksWidgetDto> FindTracksWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorInterestsWidgetDto> FindInterestsWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorInnovationEvaluationsWidgetDto> FindInnovationEvaluationsWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorMusicBandEvaluationsWidgetDto> FindMusicBandsEvaluationsWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorCartoonEvaluationsWidgetDto> FindCartoonEvaluationsWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorAudiovisualCommissionEvaluationsWidgetDto> FindAudiovisualCommissionEvaluationsWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorDto> FindConferenceWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorDto> FindParticipantsWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorApiConfigurationWidgetDto> FindApiConfigurationWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorDto> FindLogisticInfoWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<List<AttendeeCollaboratorDto>> FindExecutivesSchedulesByOrganizationsUidsAsync(List<Guid> organizationUid, int editionId);

        #endregion

        #region Networks

        Task<List<AttendeeCollaboratorNetworkDto>> FindAllExcelNetworkDtoByEditionIdAsync(int editionId);
        Task<IPagedList<AttendeeCollaboratorNetworkDto>> FindAllNetworkDtoByEditionIdPagedAsync(int editionId, string searchKeyworkds, Guid currentCollaboratorUid, Guid? collaboratorRoleUid, Guid? collaboratorIndustryUid, int page, int pageSize);

        #endregion

        #region Logistics - Availability

        Task<IPagedList<AttendeeCollaboratorBaseDto>> FindAllAvailabilitiesByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, int editionId);
        Task<int> CountAllAvailabilitiesByDataTable(bool showAllEditions, int editionId);
        Task<AttendeeCollaboratorBaseDto> FindAvailabilityDtoAsync(Guid attendeeCollaboratorUid);

        #endregion

        #region Api

        Task<List<AttendeeCollaboratorApiConfigurationWidgetDto>> FindAllApiConfigurationWidgetDtoByHighlight(int editionId, string collaboratorTypeName);
        Task<IPagedList<List>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, int page, int pageSize);
        Task<AttendeeCollaboratorTicketsInformationDto> FindUserTicketsInformationDtoByDocument(int editionId, string document);

        #endregion

        #region Producers 
        Task<List<MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto>> FindAllBaseDtoByUid(Guid attendeeCollaboratorUid, int languageId);
        Task<IPagedList<MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto>> FindAllByActiveSellerNegotiationsAndByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, int editionId, int languageId);
        Task<List<MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto>> FindAllBaseDtoByActiveSellerNegotiations(string keywords, List<Guid> selectedAttendeeOrganizationsUids, int editionId, int languageId);
        Task<int> CountAllByActiveSellerNegotiationsAndByDataTable(bool showAllEditions, int? editionId);
        #endregion
    }
}