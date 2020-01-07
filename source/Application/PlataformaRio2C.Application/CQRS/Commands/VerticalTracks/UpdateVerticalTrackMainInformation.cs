// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="UpdateVerticalTrackMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateVerticalTrackMainInformation</summary>
    public class UpdateVerticalTrackMainInformation : CreateVerticalTrack
    {
        public Guid VerticalTrackUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateVerticalTrackMainInformation"/> class.</summary>
        /// <param name="verticalTrackDto">The vertical track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateVerticalTrackMainInformation(
            VerticalTrackDto verticalTrackDto,
            List<LanguageDto> languagesDtos)
            : base(verticalTrackDto, languagesDtos)
        {
            this.VerticalTrackUid = verticalTrackDto?.VerticalTrack?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateVerticalTrackMainInformation"/> class.</summary>
        public UpdateVerticalTrackMainInformation()
        {
        }
    }
}