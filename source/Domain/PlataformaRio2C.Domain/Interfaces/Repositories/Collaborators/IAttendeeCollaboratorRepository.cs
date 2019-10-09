// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="IAttendeeCollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeCollaboratorRepository</summary>
    public interface IAttendeeCollaboratorRepository : IRepository<AttendeeCollaborator>
    {
        #region Site Widgets

        Task<AttendeeCollaboratorSiteDetailsDto> FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorSiteMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);
        Task<AttendeeCollaboratorSiteCompanyWidgetDto> FindSiteCompanyWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId);

        #endregion
    }
}