// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-24-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-25-2021
// ***********************************************************************
// <copyright file="SellerAttendeeOrganizationBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SellerAttendeeOrganizationBaseDto</summary>
    public class SellerAttendeeOrganizationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public OrganizationBaseDto OrganizationBaseDto { get; set; }
        public EditionBaseDto EditionBaseDto { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        public IEnumerable<AttendeeCollaboratorBaseDto> AttendeeCollaboratorBaseDtos { get; set; }
        public IEnumerable<NegotiationBaseDto> SellerNegotiationBaseDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SellerAttendeeOrganizationBaseDto"/> class.</summary>
        public SellerAttendeeOrganizationBaseDto()
        {
        }

        ///// <summary>Gets the display name.</summary>
        ///// <value>The display name.</value>
        //public string DisplayName
        //{
        //    get
        //    {
        //        var holdingName = OrganizationBaseDto?.HoldingBaseDto?.Name ?? string.Empty;
        //        return holdingName + (!string.IsNullOrEmpty(holdingName) ? " » " : string.Empty) + OrganizationBaseDto?.Name;
        //    }
        //}
    }
}