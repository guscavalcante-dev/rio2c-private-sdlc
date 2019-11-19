// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-18-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorDownloadDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorDownloadDto</summary>
    public class AttendeeCollaboratorDownloadDto : AttendeeCollaboratorDto
    {
        public IEnumerable<AttendeeOrganizationDto> AttendeeOrganizationsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorDownloadDto"/> class.</summary>
        public AttendeeCollaboratorDownloadDto()
        {
        }
    }
}