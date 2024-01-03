// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AttendeeOrganizationBaseCommand</summary>
    public class AttendeeOrganizationBaseCommand
    {

        [Display(Name = "Player", ResourceType = typeof(Labels))]
        [RequiredIf("IsAttendeeOrganizationUidRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AttendeeOrganizationUid { get; set; }

        public bool IsAttendeeOrganizationUidRequired { get; set; }

        public List<AttendeeOrganizationBaseDto> AttendeeOrganizationsBaseDtos { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationBaseCommand"/> class.</summary>
        /// <param name="attendeeOrganizationBaseDto">The attendee organization base dto.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        public AttendeeOrganizationBaseCommand(AttendeeOrganizationBaseDto attendeeOrganizationBaseDto, List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, bool isAttendeeOrganizationRequired=true)
        {
            this.UpdateBaseProperties(attendeeOrganizationBaseDto, attendeeOrganizationsBaseDtos, isAttendeeOrganizationRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationBaseCommand"/> class.</summary>
        public AttendeeOrganizationBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="attendeeOrganizationBaseDto">The attendee organization base dto.</param>
        /// <param name="attendeeOrganizationsBaseDtos">The attendee organizations base dtos.</param>
        private void UpdateBaseProperties(AttendeeOrganizationBaseDto attendeeOrganizationBaseDto, List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos, bool isAttendeeOrganizationRequired)
        {
            this.IsAttendeeOrganizationUidRequired = isAttendeeOrganizationRequired;
            this.AttendeeOrganizationUid = attendeeOrganizationBaseDto?.Uid;
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