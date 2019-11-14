// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-14-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SellerAttendeeOrganizationDto</summary>
    public class SellerAttendeeOrganizationDto
    {
        public SellerAttendeeOrganization SellerAttendeeOrganization { get; set; }
        public AttendeeOrganizationDto AttendeeOrganizationDto { get; set; }
        public AttendeeCollaboratorTicket AttendeeCollaboratorTicket { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SellerAttendeeOrganizationDto"/> class.</summary>
        public SellerAttendeeOrganizationDto()
        {
        }
    }
}