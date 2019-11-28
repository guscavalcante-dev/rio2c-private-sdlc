// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="IEditionRepository.cs" company="Softo">
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
    /// <summary>IEditionRepository</summary>
    public interface IEditionRepository : IRepository<Edition>
    {
        Task<Edition> FindByUidAsync(Guid editionUid, bool showInactive);
        List<Edition> FindAllByIsActive(bool showInactive);
    }    
}