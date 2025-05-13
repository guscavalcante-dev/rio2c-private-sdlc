// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationSiteTargetAudienceWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationSiteTargetAudienceWidgetDto</summary>
    public class AttendeeOrganizationSiteTargetAudienceWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }
        public IEnumerable<OrganizationTargetAudienceDto> OrganizationTargetAudiencesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationSiteTargetAudienceWidgetDto"/> class.</summary>
        public AttendeeOrganizationSiteTargetAudienceWidgetDto()
        {
        }

        /// <summary>Gets the organization target audience dto by target audience uid.</summary>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <returns></returns>
        public OrganizationTargetAudienceDto GetOrganizationTargetAudienceDtoByTargetAudienceUid(Guid targetAudienceUid)
        {
            return this.OrganizationTargetAudiencesDtos?.FirstOrDefault(otad => otad.TargetAudienceUid == targetAudienceUid);
        }
    }
}