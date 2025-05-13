// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="ISubscribeListRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>ISubscribeListRepository</summary>
    public interface ISubscribeListRepository : IRepository<SubscribeList>
    {
        Task<List<SubscribeList>> FindAllAsync();
        Task<List<SubscribeList>> FindAllByNotUids(List<Guid> subscribeListUids);
    }
}