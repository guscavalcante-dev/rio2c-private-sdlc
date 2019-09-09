// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
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
        public string SocialMedia { get; set; }
        public IEnumerable<OrganizationActivityDto> OrganizationActivitiesDtos { get; set; }
        public IEnumerable<OrganizationTargetAudienceDto> OrganizationTargetAudiencesDtos { get; set; }
        public IEnumerable<OrganizationInterestDto> OrganizationInterestsDtos { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdaterDto { get; set; }
        public AddressBaseDto AddressBaseDto { get; set; }

        public IEnumerable<OrganizationDescriptionBaseDto> DescriptionsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingDto"/> class.</summary>
        public OrganizationDto()
        {
        }
    }
}