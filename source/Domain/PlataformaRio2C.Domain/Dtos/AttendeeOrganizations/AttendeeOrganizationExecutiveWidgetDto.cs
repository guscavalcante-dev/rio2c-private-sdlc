// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-22-2021
// ***********************************************************************
// <copyright file="AttendeeOrganizationExecutiveWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationExecutiveWidgetDto</summary>
    public class AttendeeOrganizationExecutiveWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public IEnumerable<AttendeeCollaboratorDto> AttendeeCollaboratorsDtos { get; set; }
        public bool? IsInCurrentEdition { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationExecutiveWidgetDto"/> class.</summary>
        public AttendeeOrganizationExecutiveWidgetDto()
        {
        }
    }
}