// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 28-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 28-06-2021
// ***********************************************************************
// <copyright file="CreateStartup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateStartup</summary>
    public class CreateStartup : BaseCommand
    {
        public StartupApiDto StartupApiDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateStartup"/> class.</summary>
        public CreateStartup(StartupApiDto startupApiDto)
        {
            this.StartupApiDto = startupApiDto;
        }
    }
}