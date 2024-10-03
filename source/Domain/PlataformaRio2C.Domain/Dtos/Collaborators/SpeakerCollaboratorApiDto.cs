// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-16-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-21-2024
// ***********************************************************************
// <copyright file="SpeakerCollaboratorApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SpeakerCollaboratorApiDto</summary>
    public class SpeakerCollaboratorApiDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string BadgeName { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApiDisplayEnabled { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBiosDtos { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<OrganizationApiListDto> OrganizationsDtos { get; set; }
        public IEnumerable<ConferenceDto> ConferencesDtos { get; set; }
        public IEnumerable<TrackDto> TracksDtos { get; set; }

        /// <summary>
        /// Gets the social networks.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SpeakerSocialNetworkApiResponse> GetSocialNetworks()
        {
            var socialNetworks = new List<SpeakerSocialNetworkApiResponse>();

            if (!string.IsNullOrEmpty(this.Linkedin))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "LinkedIn", Url = this.Linkedin.GetLinkedinUrl() });
            }

            if (!string.IsNullOrEmpty(this.Twitter))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "Twitter", Url = this.Twitter.GetTwitterUrl() });
            }

            if (!string.IsNullOrEmpty(this.Instagram))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "Instagram", Url = this.Instagram.GetInstagramUrl() });
            }

            if (!string.IsNullOrEmpty(this.Youtube))
            {
                socialNetworks.Add(new SpeakerSocialNetworkApiResponse { Slug = "YouTube", Url = this.Youtube.GetUrlWithProtocol() });
            }

            return socialNetworks;
        }

        /// <summary>Gets the collaborator mini bio base dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public CollaboratorMiniBioBaseDto GetCollaboratorMiniBioBaseDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.MiniBiosDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == languageCode) ??
                   this.MiniBiosDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == "pt-br");
        }

        /// <summary>Gets the collaborator job title base dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetCollaboratorJobTitleBaseDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.JobTitlesDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == languageCode) ??
                   this.JobTitlesDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code == "pt-br");
        }

        /// <summary>
        /// Gets the track base API response by language code.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public IEnumerable<TrackBaseApiResponse> GetTrackBaseApiResponseByLanguageCode(string languageCode)
        {
            return this.TracksDtos?.Select(td => new TrackBaseApiResponse
            {
                Uid = td.Track.Uid,
                Name = td.Track.GetNameByLanguageCode(languageCode),
                Color = td.Track.Color
            })?.OrderBy(t => t.Name);
        }

        /// <summary>
        /// Gets the conferences API response by language code.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public IEnumerable<SpeakerConferenceApiResponse> GetConferencesApiResponseByLanguageCode(string languageCode)
        {
            return this.ConferencesDtos?.Select(c => new SpeakerConferenceApiResponse
            {
                Uid = c.Uid,
                Event = new EditionEventBaseApiResponse
                {
                    Uid = c.EditionEvent.Uid,
                    Name = c.EditionEvent.Name.GetSeparatorTranslation(languageCode, Language.Separator)?.Trim()
                },
                Title = c.GetConferenceTitleDtoByLanguageCode(languageCode)?.ConferenceTitle?.Value?.Trim(),
                Synopsis = c.GetConferenceSynopsisDtoByLanguageCode(languageCode)?.ConferenceSynopsis?.Value?.Trim(),
                Date = c.StartDate?.ToBrazilTimeZone().ToString("yyyy-MM-dd"),
                StartTime = c.StartDate?.ToBrazilTimeZone().ToString("HH:mm"),
                EndTime = c.EndDate?.ToBrazilTimeZone().ToString("HH:mm"),
                DurationMinutes = (int)((c.EndDate - c.StartDate)?.TotalMinutes ?? 0),
                Room = c.RoomDto != null ? new RoomBaseApiResponse
                {
                    Uid = c.RoomDto.Uid,
                    Name = c.RoomDto.GetRoomNameByLanguageCode(languageCode)?.RoomName?.Value?.Trim()
                } : null
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerCollaboratorApiDto" /> class.
        /// </summary>
        public SpeakerCollaboratorApiDto()
        {
        }
    }
}