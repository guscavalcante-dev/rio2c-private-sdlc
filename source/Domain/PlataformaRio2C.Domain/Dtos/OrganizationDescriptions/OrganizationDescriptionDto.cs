// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="OrganizationDescriptionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationDescriptionDto</summary>
    public class OrganizationDescriptionDto : OrganizationDescriptionBaseDto
    {
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdatedDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationDescriptionDto"/> class.</summary>
        public OrganizationDescriptionDto()
        {
        }
    }
}