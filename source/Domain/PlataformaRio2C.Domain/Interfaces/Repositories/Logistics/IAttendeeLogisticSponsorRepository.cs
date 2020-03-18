// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="IAttendeeLogisticSponsorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeLogisticSponsorRepository</summary>
    public interface IAttendeeLogisticSponsorRepository : IRepository<AttendeeLogisticSponsor>
    {
        Task<List<AttendeeLogisticSponsorJsonDto>> FindAllBaseDtosByIsOtherAsnyc(int editionDtoId, bool isOther);
        Task<AttendeeLogisticSponsorJsonDto> FindOtherDtoAsync(int editionId);
    }    
}