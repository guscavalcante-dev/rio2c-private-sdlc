// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-25-2019
// ***********************************************************************
// <copyright file="FindAllEditionsDtosAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllEditionsByIsActiveAsync</summary>
    public class FindAllEditionsDtosAsync : BaseQuery<List<EditionDto>>
    {
        public bool ShowInactive { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllEditionsDtosAsync"/> class.</summary>
        /// <param name="showInactive">The show inactive.</param>
        public FindAllEditionsDtosAsync(bool? showInactive = false)
        {
            this.ShowInactive = showInactive.Value;
        }
    }
}