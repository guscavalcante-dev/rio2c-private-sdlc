// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="IConferenceParticipantRoleRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IConferenceParticipantRoleRepository</summary>
    public interface IConferenceParticipantRoleRepository : IRepository<ConferenceParticipantRole>
    {
        Task<List<ConferenceParticipantRoleDto>> FindAllDtosAsync();
    }    
}