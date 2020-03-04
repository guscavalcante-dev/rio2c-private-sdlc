// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="IAttendeeCollaboratorRepository.cs" company="Softo">
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
    /// <summary>IAttendeeCollaboratorRepository</summary>
    public interface IAttendeeCollaboratorRepository : IRepository<AttendeeCollaborator>
    {
        #region Widgets

        Task<AttendeeCollaboratorSiteDetailsDto> FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorSiteMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorSiteCompanyWidgetDto> FindSiteCompanyWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorOnboardingInfoWidgetDto> FindOnboardingInfoWidgetDtoAsync(Guid collaboratorUid, Guid collaboratorTypeUid, int editionId);
        Task<AttendeeCollaboratorDto> FindConferenceWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorDto> FindParticipantsWidgetDtoAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorApiConfigurationWidgetDto> FindApiConfigurationWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);

        #endregion

        #region Networks

        Task<List<AttendeeCollaboratorNetworkDto>> FindAllExcelNetworkDtoByEditionIdAsync(int editionId);
        Task<IPagedList<AttendeeCollaboratorNetworkDto>> FindAllNetworkDtoByEditionIdPagedAsync(int editionId, string searchKeyworkds, Guid? collaboratorRoleUid, Guid? collaboratorIndustryUid, int page, int pageSize);

        #endregion

        #region Api
        Task<List<AttendeeCollaboratorApiConfigurationWidgetDto>> FindAllApiConfigurationWidgetDtoByHighlight(int editionId, string collaboratorTypeName);

        Task<IPagedList<AttendeeCollaboratorListDto>> FindAllDropdownApiListDtoPaged(int editionId, int page, int pageSize);

        #endregion
    }
}