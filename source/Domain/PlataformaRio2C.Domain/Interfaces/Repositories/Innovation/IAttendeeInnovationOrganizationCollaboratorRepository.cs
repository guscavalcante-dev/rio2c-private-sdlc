// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="IAttendeeInnovationOrganizationCollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeInnovationOrganizationCollaboratorRepository</summary>
    public interface IAttendeeInnovationOrganizationCollaboratorRepository : IRepository<AttendeeInnovationOrganizationCollaborator>
    {
        Task<AttendeeInnovationOrganizationCollaborator> FindByIdAsync(int attendeeInnovationOrganizationCollaboratorId);
        Task<AttendeeInnovationOrganizationCollaborator> FindByUidAsync(Guid attendeeInnovationOrganizationCollaboratorUid);
        Task<List<AttendeeInnovationOrganizationCollaborator>> FindAllByIdsAsync(List<int?> attendeeInnovationOrganizationCollaboratorIds);
        Task<List<AttendeeInnovationOrganizationCollaborator>> FindAllByUidsAsync(List<Guid?> attendeeInnovationOrganizationCollaboratorUids);
    }
}