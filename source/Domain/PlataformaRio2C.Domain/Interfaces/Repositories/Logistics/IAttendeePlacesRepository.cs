// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="IAttendeePlacesRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeePlacesRepository</summary>
    public interface IAttendeePlacesRepository : IRepository<AttendeePlace>
    {
        Task<List<AttendeePlaceDropdownDto>> FindAllDropdownDtosAsync(int editionId, bool? isHotel = null, bool? isAirport = null);
    }
}