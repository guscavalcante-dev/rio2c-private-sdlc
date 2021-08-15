// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-08-2021
// ***********************************************************************
// <copyright file="AudiovisualCollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AudiovisualCollaboratorBaseCommand</summary>
    public class AudiovisualCollaboratorBaseCommand : CollaboratorBaseCommand
    {
        //TODO: Essa lista tem que ser AttendeeCollaboratorInterestBaseCommand
        [Display(Name = "Interests", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<dynamic> AttendeeCollaboratorInterests { get; set; }
        //public List<AttendeeCollaboratorInterestBaseCommand> AttendeeCollaboratorInterests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudiovisualCollaboratorBaseCommand" /> class.
        /// </summary>
        public AudiovisualCollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        public void UpdateBaseProperties(
            AttendeeCollaboratorTracksWidgetDto entity, 
            List<Interest> interests)
        {
            this.UpdateAudiovisualInterests(entity, interests);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="interests">The innovation organization track options.</param>
        public void UpdateDropdownProperties(
            List<Interest> interests)
        {
            this.UpdateAudiovisualInterests(null, interests);
        }

        /// <summary>
        /// Updates the innovation organization track options.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interests">The innovation organization track options.</param>
        private void UpdateAudiovisualInterests(
            AttendeeCollaboratorTracksWidgetDto entity, 
            List<Interest> interests)
        {
            //this.AttendeeCollaboratorInterests = new List<AttendeeCollaboratorInterestBaseCommand>();
            //foreach (var interest in interests)
            //{
            //    var attendeeCollaboratorAudiovisualOrganizationTrackDto = entity?.AttendeeCollaboratorAudiovisualOrganizationTrackDtos?.FirstOrDefault(aot => aot.AudiovisualOrganizationTrackOption.Uid == interest.Uid);
            //    this.AttendeeCollaboratorInterests.Add(attendeeCollaboratorAudiovisualOrganizationTrackDto != null ? new AttendeeAudiovisualOrganizationTrackBaseCommand(attendeeCollaboratorAudiovisualOrganizationTrackDto) :
            //                                                                                                new AttendeeAudiovisualOrganizationTrackBaseCommand(interest));
            //}
        }
    }
}