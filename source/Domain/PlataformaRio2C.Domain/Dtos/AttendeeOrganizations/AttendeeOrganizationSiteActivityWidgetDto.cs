// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationSiteActivityWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationSiteActivityWidgetDto</summary>
    public class AttendeeOrganizationSiteActivityWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }
        public IEnumerable<OrganizationActivityDto> OrganizationActivitiesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationSiteActivityWidgetDto"/> class.</summary>
        public AttendeeOrganizationSiteActivityWidgetDto()
        {
        }

        /// <summary>Gets the organization activity dto by activity uid.</summary>
        /// <param name="activityUid">The activity uid.</param>
        /// <returns></returns>
        public OrganizationActivityDto GetOrganizationActivityDtoByActivityUid(Guid activityUid)
        {
            return this.OrganizationActivitiesDtos?.FirstOrDefault(oad => oad.ActivityUid == activityUid);
        }
    }
}