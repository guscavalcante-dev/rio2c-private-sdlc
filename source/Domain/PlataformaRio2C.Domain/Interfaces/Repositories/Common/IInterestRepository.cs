// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="IInterestRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IInterestRepository</summary>
    public interface IInterestRepository : IRepository<Interest>
    {
        Task<List<InterestDto>> FindAllDtosbyProjectTypeIdAsync(int projectTypeId);
        Task<List<InterestDto>> FindAllDtosByInterestGroupUidAsync(Guid interestGroupUid);
        Task<List<IGrouping<InterestGroup, Interest>>> FindAllByProjectTypeIdAndGroupedByInterestGroupAsync(int projectTypeId);
        Task<List<Interest>> FindAllByUidsAsync(List<Guid> interestsUids);
        Task<List<Interest>> FindAllByInterestGroupUidAsync(Guid interestGroupUid);
        Task<List<Interest>> FindAllByAttendeeCollaboratorIdAsync(int attendeeCollaboratorId);
    }
}
