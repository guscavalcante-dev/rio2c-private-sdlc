// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorSiteCompanyWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorSiteCompanyWidgetDto</summary>
    public class AttendeeCollaboratorSiteCompanyWidgetDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }

        public IEnumerable<AttendeeOrganizationDto> AttendeeOrganizationsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorSiteCompanyWidgetDto"/> class.</summary>
        public AttendeeCollaboratorSiteCompanyWidgetDto()
        {
        }
    }
}