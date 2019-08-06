// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="EditionService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Services
{
    /// <summary>EditionService</summary>
    public class EditionService : Service<Edition>, IEditionService
    {
        public EditionService(IEditionRepository repository)
            :base(repository)
        {
        }
    }
}