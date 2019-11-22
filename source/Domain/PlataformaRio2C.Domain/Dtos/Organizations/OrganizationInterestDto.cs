// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="OrganizationInterestDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationInterestDto</summary>
    public class OrganizationInterestDto
    {
        public OrganizationInterest OrganizationInterest { get; set; }
        public Interest Interest { get; set; }
        public InterestGroup InterestGroup { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationInterestDto"/> class.</summary>
        public OrganizationInterestDto()
        {
        }
    }
}