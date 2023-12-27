// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
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
        public string OrganizationTargetAudienceAdditionalInfo { get; set; }

        public int TargetAudienceId { get; set; }
        public Guid TargetAudienceUid { get; set; }
        public string TargetAudienceName { get; set; }
        public bool TargetAudienceHasAdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudienceDto"/> class.</summary>
        public OrganizationTargetAudienceDto()
        {
        }
    }
}