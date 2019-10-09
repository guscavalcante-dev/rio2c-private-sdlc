// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationTypeDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationTypeDto</summary>
    public class AttendeeOrganizationTypeDto
    {
        public AttendeeOrganizationType AttendeeOrganizationType { get; set; }
        public OrganizationType OrganizationType { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationTypeDto"/> class.</summary>
        public AttendeeOrganizationTypeDto()
        {
        }
    }
}