// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-22-2021
// ***********************************************************************
// <copyright file="AttendeeOrganizationSiteDetailsDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationMainInformationWidgetDto</summary>
    public class AttendeeOrganizationSiteDetailsDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }
        public IEnumerable<AttendeeOrganizationTypeDto> AttendeeOrganizationTypesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationSiteDetailsDto"/> class.</summary>
        public AttendeeOrganizationSiteDetailsDto()
        {
        }

        /// <summary>Determines whether [has any type] [the specified organization types].</summary>
        /// <param name="organizationTypes">The organization types.</param>
        /// <returns>
        ///   <c>true</c> if [has any type] [the specified organization types]; otherwise, <c>false</c>.</returns>
        public bool HasAnyType(string[] organizationTypes)
        {
            return organizationTypes?.Any() == true && this.AttendeeOrganizationTypesDtos.Any(aotd => organizationTypes.Contains(aotd.OrganizationType.Name));
        }
    }
}