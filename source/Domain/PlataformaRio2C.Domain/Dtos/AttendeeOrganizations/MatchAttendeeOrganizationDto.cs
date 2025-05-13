// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-17-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-17-2019
// ***********************************************************************
// <copyright file="MatchAttendeeOrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MatchAttendeeOrganizationDto</summary>
    public class MatchAttendeeOrganizationDto : AttendeeOrganizationDto
    {
        public IEnumerable<InterestGroup> InterestGroupsMatches { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MatchAttendeeOrganizationDto"/> class.</summary>
        public MatchAttendeeOrganizationDto()
        {
        }

        /// <summary>Gets the total interest groups matches count.</summary>
        /// <returns></returns>
        public int GetTotalInterestGroupsMatchesCount()
        {
            return this.InterestGroupsMatches?.Count() ?? 0;
        }
    }
}