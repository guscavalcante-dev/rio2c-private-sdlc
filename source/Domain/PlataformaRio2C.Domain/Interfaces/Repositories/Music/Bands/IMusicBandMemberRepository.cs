// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="IMusicBandMemberRepository.cs" company="Softo">
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
    /// <summary>IMusicBandMemberRepository</summary>
    public interface IMusicBandMemberRepository : IRepository<MusicBandMember>
    {
        Task<MusicBandMember> FindByIdAsync(int bandMemberId);

        Task<MusicBandMember> FindByUidAsync(Guid bandMemberUid);

        Task<List<MusicBandMember>> FindAllByBandIdAsync(int musicBandId);
    }
}