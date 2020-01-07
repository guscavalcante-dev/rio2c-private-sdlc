// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateHorizontalTrackMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateHorizontalTrackMainInformation</summary>
    public class UpdateHorizontalTrackMainInformation : CreateHorizontalTrack
    {
        public Guid HorizontalTrackUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateHorizontalTrackMainInformation"/> class.</summary>
        /// <param name="horizontalTrackDto">The horizontal track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateHorizontalTrackMainInformation(
            HorizontalTrackDto horizontalTrackDto,
            List<LanguageDto> languagesDtos)
            : base(horizontalTrackDto, languagesDtos)
        {
            this.HorizontalTrackUid = horizontalTrackDto?.HorizontalTrack?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateHorizontalTrackMainInformation"/> class.</summary>
        public UpdateHorizontalTrackMainInformation()
        {
        }
    }
}