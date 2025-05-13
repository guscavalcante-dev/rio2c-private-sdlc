// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-04-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-04-2023
// ***********************************************************************
// <copyright file="AttendeeOrganizationTemplateBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AttendeeOrganizationTemplateBaseCommand</summary>
    public class TemplateAttendeeOrganizationBaseCommand
    {
        public Guid AttendeeOrganizationUid { get; set; }

        public List<AttendeeOrganizationBaseDto> AttendeeOrganizationsBaseDtos { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="TemplateAttendeeOrganizationBaseCommand"/> class.</summary>
        /// <param name="attendeeOrganizationBaseDto">The attendee organization base dto.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        public TemplateAttendeeOrganizationBaseCommand(
            AttendeeOrganizationBaseDto attendeeOrganizationBaseDto,
            List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos)
        {
            this.UpdateBaseProperties(attendeeOrganizationBaseDto, attendeeOrganizationsBaseDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="TemplateAttendeeOrganizationBaseCommand"/> class.</summary>
        public TemplateAttendeeOrganizationBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="attendeeOrganizationBaseDto">The attendee organization base dto.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        private void UpdateBaseProperties(AttendeeOrganizationBaseDto attendeeOrganizationBaseDto, List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos)
        {
            this.AttendeeOrganizationUid = attendeeOrganizationBaseDto?.Uid ?? Guid.Empty;
            this.UpdateDropdownProperties(attendeeOrganizationsBaseDtos);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        public void UpdateDropdownProperties(List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos)
        {
            this.AttendeeOrganizationsBaseDtos = attendeeOrganizationsBaseDtos;
        }
    }
}