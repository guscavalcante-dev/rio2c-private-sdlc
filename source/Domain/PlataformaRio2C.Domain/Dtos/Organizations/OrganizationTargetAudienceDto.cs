// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="OrganizationTargetAudienceDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationTargetAudienceDto</summary>
    public class OrganizationTargetAudienceDto
    {
        public int OrganizationTargetAudienceId { get; set; }
        public Guid OrganizationTargetAudienceUid { get; set; }
        public int TargetAudienceId { get; set; }
        public Guid TargetAudienceUid { get; set; }
        public string TargetAudienceName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudienceDto"/> class.</summary>
        public OrganizationTargetAudienceDto()
        {
        }
    }
}