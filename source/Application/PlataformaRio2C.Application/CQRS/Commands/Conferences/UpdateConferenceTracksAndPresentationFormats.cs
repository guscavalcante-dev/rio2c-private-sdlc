// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="UpdateConferenceTracksAndPresentationFormats.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateConferenceTracksAndPresentationFormats</summary>
    public class UpdateConferenceTracksAndPresentationFormats : BaseCommand
    {
        public Guid ConferenceUid { get; set; }

        public List<Guid> TrackUids { get; set; }
        public List<Guid> PresentationFormatUids { get; set; }

        public List<Track> Tracks { get; private set; }
        public List<PresentationFormat> PresentationFormats { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceTracksAndPresentationFormats"/> class.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="tracks">The tracks.</param>
        /// <param name="presentationFormats">The presentation formats.</param>
        public UpdateConferenceTracksAndPresentationFormats(
            ConferenceDto conferenceDto,
            List<Track> tracks,
            List<PresentationFormat> presentationFormats)
        {
            this.ConferenceUid = conferenceDto?.Conference?.Uid ?? Guid.Empty;
            this.TrackUids = conferenceDto?.ConferenceTrackDtos?.Select(cvtd => cvtd.Track.Uid)?.ToList();
            this.PresentationFormatUids = conferenceDto?.ConferencePresentationFormatDtos?.Select(chtd => chtd.PresentationFormat.Uid)?.ToList();

            this.UpdateDropdowns(tracks, presentationFormats);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceTracksAndPresentationFormats"/> class.</summary>
        public UpdateConferenceTracksAndPresentationFormats()
        {
        }

        /// <summary>Updates the dropdowns.</summary>
        /// <param name="tracks">The tracks.</param>
        /// <param name="presentationFormats">The presentation formats.</param>
        public void UpdateDropdowns(
            List<Track> tracks,
            List<PresentationFormat> presentationFormats)
        {
            this.Tracks = tracks;
            this.PresentationFormats = presentationFormats;
        }
    }
}