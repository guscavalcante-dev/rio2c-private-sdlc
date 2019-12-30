// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-18-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorApiConfigurationWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorApiConfigurationWidgetDto</summary>
    public class AttendeeCollaboratorApiConfigurationWidgetDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public User User { get; set; }

        public IEnumerable<AttendeeCollaboratorTypeDto> AttendeeCollaboratorTypeDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorApiConfigurationWidgetDto"/> class.</summary>
        public AttendeeCollaboratorApiConfigurationWidgetDto()
        {
        }

        /// <summary>Gets the name of the attendee collaborator type dto by collaborator type.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        public AttendeeCollaboratorTypeDto GetAttendeeCollaboratorTypeDtoByCollaboratorTypeName(string collaboratorTypeName)
        {
            return this.AttendeeCollaboratorTypeDtos?
                            .FirstOrDefault(act => act.CollaboratorType.Name == collaboratorTypeName);
        }
    }
}