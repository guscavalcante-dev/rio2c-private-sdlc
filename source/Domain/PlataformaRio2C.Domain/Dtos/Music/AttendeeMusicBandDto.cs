// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="AttendeeMusicBandDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeMusicBandDto</summary>
    public class AttendeeMusicBandDto
    {
        public AttendeeMusicBand AttendeeMusicBand { get; set; }
        public MusicBand MusicBand { get; set; }
        public MusicBandType MusicBandType { get; set; }
        public AttendeeMusicBandCollaboratorDto AttendeeMusicBandCollaboratorDto { get; set; }

        public IEnumerable<MusicBandGenreDto> MusicBandGenreDtos { get; set; }
        public IEnumerable<MusicBandTargetAudienceDto> MusicBandTargetAudienceDtos { get; set; }
        public IEnumerable<MusicBandMember> MusicBandMembers { get; set; }
        public IEnumerable<MusicBandTeamMember> MusicBandTeamMembers { get; set; }
        public IEnumerable<ReleasedMusicProject> ReleasedMusicProjects { get; set; }


        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandDto"/> class.</summary>
        public AttendeeMusicBandDto()
        {
        }

        //#region Target Audiences

        ///// <summary>Gets the target audience dto by target audience uid.</summary>
        ///// <param name="targetAudienceUid">The target audience uid.</param>
        ///// <returns></returns>
        //public ProjectTargetAudienceDto GetTargetAudienceDtoByTargetAudienceUid(Guid targetAudienceUid)
        //{
        //    return this.ProjectTargetAudienceDtos?.FirstOrDefault(ptad => ptad.TargetAudience.Uid == targetAudienceUid);
        //}

        //#endregion

        //#region Translations

        ///// <summary>Gets the title dto by language code.</summary>
        ///// <param name="culture">The culture.</param>
        ///// <returns></returns>
        //public ProjectTitleDto GetTitleDtoByLanguageCode(string culture)
        //{
        //    return this.ProjectTitleDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        //}

        ///// <summary>Gets the log line dto by language code.</summary>
        ///// <param name="culture">The culture.</param>
        ///// <returns></returns>
        //public ProjectLogLineDto GetLogLineDtoByLanguageCode(string culture)
        //{
        //    return this.ProjectLogLineDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        //}

        ///// <summary>Gets the summary dto by language code.</summary>
        ///// <param name="culture">The culture.</param>
        ///// <returns></returns>
        //public ProjectSummaryDto GetSummaryDtoByLanguageCode(string culture)
        //{
        //    return this.ProjectSummaryDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        //}

        ///// <summary>Gets the production plan dto by language code.</summary>
        ///// <param name="culture">The culture.</param>
        ///// <returns></returns>
        //public ProjectProductionPlanDto GetProductionPlanDtoByLanguageCode(string culture)
        //{
        //    return this.ProjectProductionPlanDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        //}

        ///// <summary>Gets the additional information dto by language code.</summary>
        ///// <param name="culture">The culture.</param>
        ///// <returns></returns>
        //public ProjectAdditionalInformationDto GetAdditionalInformationDtoByLanguageCode(string culture)
        //{
        //    return this.ProjectAdditionalInformationDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        //}

        //#endregion
    }
}