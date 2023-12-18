// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-16-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="SpeakerCollaboratorApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.ApiModels;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SpeakerCollaboratorApiDto</summary>
    public class SpeakerCollaboratorApiDto : CollaboratorApiListDto
    {
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }

        public IEnumerable<TrackDto> TracksDtos { get; set; }
        public IEnumerable<ConferenceDto> ConferencesDtos { get; set; }

        /// <summary>
        /// Gets the social networks.
        /// </summary>
        /// <returns></returns>
        public List<SpeakerSocialNetworkApiResponse> GetSocialNetworks()
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

        /// <summary>Initializes a new instance of the <see cref="SpeakerCollaboratorApiDto"/> class.</summary>
        public SpeakerCollaboratorApiDto()
        {
        }
    }
}