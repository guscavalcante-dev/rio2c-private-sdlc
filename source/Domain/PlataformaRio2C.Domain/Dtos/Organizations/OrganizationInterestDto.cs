// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="OrganizationInterestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationInterestDto</summary>
    public class OrganizationInterestDto
    {
        public int OrganizationInterestId { get; set; }
        public Guid OrganizationInterestUid { get; set; }

        public int InterestGroupId { get; set; }
        public Guid InterestGroupUid { get; set; }
        public string InterestGroupName { get; set; }

        public int InterestId { get; set; }
        public Guid InterestUid { get; set; }
        public string InterestName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationInterestDto"/> class.</summary>
        public OrganizationInterestDto()
        {
        }
    }
}