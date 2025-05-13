// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateTrackMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateTrackMainInformation</summary>
    public class UpdateTrackMainInformation : CreateTrack
    {
        public Guid TrackUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateTrackMainInformation"/> class.</summary>
        /// <param name="trackDto">The track dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateTrackMainInformation(
            TrackDto trackDto,
            List<LanguageDto> languagesDtos)
            : base(trackDto, languagesDtos)
        {
            this.TrackUid = trackDto?.Track?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateTrackMainInformation"/> class.</summary>
        public UpdateTrackMainInformation()
        {
        }
    }
}