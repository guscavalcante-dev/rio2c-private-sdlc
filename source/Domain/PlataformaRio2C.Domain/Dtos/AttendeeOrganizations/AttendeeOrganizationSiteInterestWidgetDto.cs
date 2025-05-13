// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationSiteInterestWidgetDto.cs" company="Softo">
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
    /// <summary>AttendeeOrganizationSiteInterestWidgetDto</summary>
    public class AttendeeOrganizationSiteInterestWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }
        public IEnumerable<OrganizationRestrictionSpecificBaseDto> RestrictionSpecificDtos { get; set; }
        public IEnumerable<OrganizationInterestDto> OrganizationInterestDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationSiteInterestWidgetDto"/> class.</summary>
        public AttendeeOrganizationSiteInterestWidgetDto()
        {
        }

        /// <summary>Gets the restriction specific dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public OrganizationRestrictionSpecificBaseDto GetRestrictionSpecificDtoByLanguageCode(string culture)
        {
            return this.RestrictionSpecificDtos?.FirstOrDefault(dd => dd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the organization interest dto by interest uid.</summary>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        public OrganizationInterestDto GetOrganizationInterestDtoByInterestUid(Guid interestUid)
        {
            return this.OrganizationInterestDtos?.FirstOrDefault(oid => oid.Interest.Uid == interestUid);
        }
    }
}