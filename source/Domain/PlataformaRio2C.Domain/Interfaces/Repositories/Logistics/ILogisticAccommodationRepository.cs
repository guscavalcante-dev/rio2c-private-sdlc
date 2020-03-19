// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-19-2020
// ***********************************************************************
// <copyright file="ILogisticAccommodationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ILogisticAccommodationRepository</summary>
    public interface ILogisticAccommodationRepository : IRepository<LogisticAccommodation>
    {
        Task<List<LogisticAccommodationDto>> FindAllDtosAsync(Guid logisticsUid);
    }    
}