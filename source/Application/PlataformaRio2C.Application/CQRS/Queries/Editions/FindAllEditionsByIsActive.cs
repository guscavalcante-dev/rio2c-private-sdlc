// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-25-2019
// ***********************************************************************
// <copyright file="FindEditionByCurrent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MediatR;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllEditionsByIsActive</summary>
    public class FindAllEditionsByIsActive : IRequest<List<EditionDto>>
    {
        public bool ShowInactive { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllEditionsByIsActive"/> class.</summary>
        /// <param name="showInactive">The show inactive.</param>
        public FindAllEditionsByIsActive(bool? showInactive = false)
        {
            this.ShowInactive = showInactive.Value;
        }
    }
}