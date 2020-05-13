// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="CleanUpConnectionsAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CleanUpConnectionsAsync</summary>
    public class CleanUpConnectionsAsync : IRequest<AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="CleanUpConnectionsAsync"/> class.</summary>
        public CleanUpConnectionsAsync()
        {
        }
    }
}