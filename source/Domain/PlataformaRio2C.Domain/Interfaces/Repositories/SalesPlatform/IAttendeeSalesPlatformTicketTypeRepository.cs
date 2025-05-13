// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-25-2019
// ***********************************************************************
// <copyright file="IAttendeeSalesPlatformTicketTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeSalesPlatformTicketTypeRepository</summary>
    public interface IAttendeeSalesPlatformTicketTypeRepository : IRepository<AttendeeSalesPlatformTicketType>
    {
        Task<List<AttendeeSalesPlatformTicketType>> FindAllByEditionIdAsync(int editionId);

    }
}