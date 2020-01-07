// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateConferenceTracks.cs" company="Softo">
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
    /// <summary>UpdateConferenceTracks</summary>
    public class UpdateConferenceTracks : BaseCommand
    {
        public Guid ConferenceUid { get; set; }

        public List<Guid> TrackUids { get; set; }
        public List<Guid> HorizontalTrackUids { get; set; }

        public List<Track> Tracks { get; private set; }
        public List<HorizontalTrack> HorizontalTracks { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceTracks"/> class.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="tracks">The tracks.</param>
        /// <param name="horizontalTracks">The horizontal tracks.</param>
        public UpdateConferenceTracks(
            ConferenceDto conferenceDto,
            List<Track> tracks,
            List<HorizontalTrack> horizontalTracks)
        {
            this.ConferenceUid = conferenceDto?.Conference?.Uid ?? Guid.Empty;
            this.TrackUids = conferenceDto?.ConferenceTrackDtos?.Select(cvtd => cvtd.Track.Uid)?.ToList();
            this.HorizontalTrackUids = conferenceDto?.ConferenceHorizontalTrackDtos?.Select(chtd => chtd.HorizontalTrack.Uid)?.ToList();

            this.UpdateDropdownProperties(tracks, horizontalTracks);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceTracks"/> class.</summary>
        public UpdateConferenceTracks()
        {
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="tracks">The tracks.</param>
        /// <param name="horizontalTracks">The horizontal tracks.</param>
        public void UpdateDropdownProperties(
            List<Track> tracks,
            List<HorizontalTrack> horizontalTracks)
        {
            this.Tracks = tracks;
            this.HorizontalTracks = horizontalTracks;
        }
    }
}