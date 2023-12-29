// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-28-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorActivityBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AttendeeCollaboratorActivityBaseCommand</summary>
    public class AttendeeCollaboratorActivityBaseCommand
    {
        public Guid ActivityUid { get; set; }
        public string ActivityName { get; set; }
        public bool ActivityHasAdditionalInfo { get; set; }
        public bool IsChecked { get; set; }

        [Display(Name = "AdditionalInformation", ResourceType = typeof(Labels))]
        public string AdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorActivityBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public AttendeeCollaboratorActivityBaseCommand(AttendeeCollaboratorActivityDto entity)
        {
            this.ActivityUid = entity.ActivityUid;
            this.ActivityName = entity.ActivityName;
            this.ActivityHasAdditionalInfo = entity.ActivityHasAdditionalInfo;
            this.AdditionalInfo = entity.AttendeeCollaboratorActivityAdditionalInfo;
            this.IsChecked = true;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorActivityBaseCommand"/> class.</summary>
        /// <param name="activity">The activity.</param>
        public AttendeeCollaboratorActivityBaseCommand(Activity activity)
        {
            this.ActivityUid = activity.Uid;
            this.ActivityName = activity.Name;
            this.ActivityHasAdditionalInfo = activity.HasAdditionalInfo;
            this.IsChecked = false;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorActivityBaseCommand"/> class.</summary>
        public AttendeeCollaboratorActivityBaseCommand()
        {
        }
    }
}