// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="OrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationDto</summary>
    public class OrganizationDto : OrganizationBaseDto
    {
        public string CompanyName { get; set; }
        public string TradeName { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdaterDto { get; set; }
        public AddressBaseDto AddressBaseDto { get; set; }

        public IEnumerable<OrganizationActivityDto> OrganizationActivitiesDtos { get; set; }
        public IEnumerable<OrganizationTargetAudienceDto> OrganizationTargetAudiencesDtos { get; set; }
        public IEnumerable<OrganizationInterestDto> OrganizationInterestDtos { get; set; }
        public IEnumerable<OrganizationDescriptionBaseDto> DescriptionsDtos { get; set; }
        public IEnumerable<OrganizationRestrictionSpecificBaseDto> RestrictionSpecificsDtos { get; set; }
        public IEnumerable<CollaboratorDto> CollaboratorsDtos { get; set; }
        public IEnumerable<AttendeeOrganizationTypeDto> AttendeeOrganizationTypesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingDto"/> class.</summary>
        public OrganizationDto()
        {
        }
    }
}