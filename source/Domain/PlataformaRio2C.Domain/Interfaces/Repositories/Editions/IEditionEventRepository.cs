// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="IEditionEventRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IEditionEventRepository</summary>
    public interface IEditionEventRepository : IRepository<EditionEvent>
    {
        Task<EditionEvent> FindByUidAsync(Guid editionEventUid);
        Task<List<EditionEvent>> FindAllByEditionIdAsync(int editionId);
    }    
}