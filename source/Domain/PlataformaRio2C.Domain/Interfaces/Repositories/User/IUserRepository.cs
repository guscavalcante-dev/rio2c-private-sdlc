// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-04-2019
// ***********************************************************************
// <copyright file="IUserRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IUserRepository</summary>
    public interface IUserRepository : IRepository<User>
    {
        UserAccessControlDto FindAccessControlDtoByUserIdAndByEditionId(int userId, int editionId);
    }    
}