// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-16-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="SpeakerCollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SpeakerCollaboratorDto</summary>
    public class SpeakerCollaboratorDto : CollaboratorApiListDto
    {
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }

        public IEnumerable<TrackDto> TracksDtos { get; set; }
        public IEnumerable<ConferenceDto> ConferencesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SpeakerCollaboratorDto"/> class.</summary>
        public SpeakerCollaboratorDto()
        {
        }
    }
}