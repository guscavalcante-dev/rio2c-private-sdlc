// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="IQuizRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IQuizRepository</summary>
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<Quiz> FindActiveAsync();
    }
}
