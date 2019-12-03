// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorNetworkDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorNetworkDto</summary>
    public class AttendeeCollaboratorNetworkDto : AttendeeCollaboratorDto
    {
        public User User { get; set; }
        public IEnumerable<AttendeeOrganizationDto> AttendeeOrganizationsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorNetworkDto"/> class.</summary>
        public AttendeeCollaboratorNetworkDto()
        {
        }
    }
}