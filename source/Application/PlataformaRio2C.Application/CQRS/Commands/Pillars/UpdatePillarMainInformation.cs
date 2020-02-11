// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdatePillarMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdatePillarMainInformation</summary>
    public class UpdatePillarMainInformation : CreatePillar
    {
        public Guid PillarUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdatePillarMainInformation"/> class.</summary>
        /// <param name="trackDto">The track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdatePillarMainInformation(
            PillarDto trackDto,
            List<LanguageDto> languagesDtos)
            : base(trackDto, languagesDtos)
        {
            this.PillarUid = trackDto?.Pillar?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdatePillarMainInformation"/> class.</summary>
        public UpdatePillarMainInformation()
        {
        }
    }
}