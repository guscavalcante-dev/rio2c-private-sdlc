// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-20-2021
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
        Task<List<InterestDto>> FindAllDtosAsync();
        Task<List<InterestDto>> FindAllDtosByInterestGroupUidAsync(Guid interestGroupUid);
        Task<List<IGrouping<InterestGroup, Interest>>> FindAllGroupedByInterestGroupsAsync();
        Task<List<Interest>> FindAllByUidsAsync(List<Guid> interestsUids);
        Task<List<Interest>> FindAllByInterestGroupUidAsync(Guid interestGroupUid);
    }
}
