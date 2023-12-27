// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-12-2023
// ***********************************************************************
// <copyright file="OrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationDto</summary>
    public class OrganizationDto : OrganizationBaseDto
    {
        public IEnumerable<OrganizationActivityDto> OrganizationActivitiesDtos { get; set; }
        public IEnumerable<OrganizationTargetAudienceDto> OrganizationTargetAudiencesDtos { get; set; }
        public IEnumerable<OrganizationInterestDto> OrganizationInterestDtos { get; set; }
        public IEnumerable<CollaboratorDto> CollaboratorsDtos { get; set; }
        public IEnumerable<AttendeeOrganizationTypeDto> AttendeeOrganizationTypesDtos { get; set; }
        public IEnumerable<InnovationOrganizationTrackOptionGroupDto> InnovationOrganizationTrackOptionGroupDtos { get; set; }

        #region Helpers

        /// <summary>
        /// Gets all interests by interest group uid.
        /// </summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <returns></returns>
        public List<OrganizationInterestDto> GetAllInterestsByInterestGroupUid(Guid interestGroupUid)
        {
            return this.OrganizationInterestDtos?.Where(oiDto => oiDto.InterestGroup.Uid == interestGroupUid)?.ToList();
        }

        /// <summary>
        /// Gets all interests names by interest group uid and culture.
        /// </summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public string GetAllInterestsNamesByInterestGroupUidAndCulture(Guid interestGroupUid, string culture)
        {
            return this.OrganizationInterestDtos
                            ?.Where(oiDto => oiDto.InterestGroup.Uid == interestGroupUid)
                            ?.Select(oiDto => oiDto.Interest.Name.GetSeparatorTranslation(culture, '|'))
                            ?.ToString("; ");
        }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="HoldingDto"/> class.</summary>
        public OrganizationDto()
        {
        }
    }
}