// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="OrganizationRestrictionSpecificDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationRestrictionSpecificDto</summary>
    public class OrganizationRestrictionSpecificDto : OrganizationRestrictionSpecificBaseDto
    {
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdatedDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationRestrictionSpecificDto"/> class.</summary>
        public OrganizationRestrictionSpecificDto()
        {
        }
    }
}