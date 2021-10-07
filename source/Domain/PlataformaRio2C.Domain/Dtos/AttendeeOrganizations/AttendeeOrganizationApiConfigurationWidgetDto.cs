// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-06-2021
// ***********************************************************************
// <copyright file="AttendeeOrganizationApiConfigurationWidgetDto.cs" company="Softo">
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
    /// <summary>AttendeeOrganizationApiConfigurationWidgetDto</summary>
    public class AttendeeOrganizationApiConfigurationWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }

        public IEnumerable<AttendeeOrganizationTypeDto> AttendeeOrganizationTypeDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationApiConfigurationWidgetDto"/> class.</summary>
        public AttendeeOrganizationApiConfigurationWidgetDto()
        {
        }

        /// <summary>
        /// Gets the name of the attendee organization type dto by organization type.
        /// </summary>
        /// <param name="organizationTypeName">Name of the organization type.</param>
        /// <returns></returns>
        public AttendeeOrganizationTypeDto GetAttendeeOrganizationTypeDtoByOrganizationTypeName(string organizationTypeName)
        {
            return this.AttendeeOrganizationTypeDtos?
                            .FirstOrDefault(act => act.OrganizationType.Name == organizationTypeName);
        }

        /// <summary>
        /// Gets the attendee organization type dto by organization type uid.
        /// </summary>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        public AttendeeOrganizationTypeDto GetAttendeeOrganizationTypeDtoByOrganizationTypeUid(Guid organizationTypeUid)
        {
            return this.AttendeeOrganizationTypeDtos?
                            .FirstOrDefault(act => act.OrganizationType.Uid == organizationTypeUid);
        }
    }
}