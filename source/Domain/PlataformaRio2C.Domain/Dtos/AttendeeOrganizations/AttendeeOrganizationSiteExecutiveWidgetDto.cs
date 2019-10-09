// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationSiteActivityWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationSiteExecutiveWidgetDto</summary>
    public class AttendeeOrganizationSiteExecutiveWidgetDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public IEnumerable<AttendeeCollaboratorDto> AttendeeCollaboratorsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationSiteExecutiveWidgetDto"/> class.</summary>
        public AttendeeOrganizationSiteExecutiveWidgetDto()
        {
        }
    }
}