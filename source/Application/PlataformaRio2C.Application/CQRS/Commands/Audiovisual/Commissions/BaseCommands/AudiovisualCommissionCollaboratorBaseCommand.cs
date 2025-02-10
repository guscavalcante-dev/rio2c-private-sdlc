// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-08-2021
// ***********************************************************************
// <copyright file="AudiovisualCommissionCollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class AudiovisualCommissionCollaboratorBaseCommand : CollaboratorBaseCommand
    {
        [Display(Name = "SubGenre", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public InterestBaseCommand[][] Interests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudiovisualCommissionCollaboratorBaseCommand" /> class.
        /// </summary>
        public AudiovisualCommissionCollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeCollaboratorInterestsWidgetDto">The entity.</param>
        /// <param name="interestsDtos">The interests.</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity,
            AttendeeCollaboratorInterestsWidgetDto attendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            base.UpdateBaseProperties(entity);
            this.UpdateInterests(attendeeCollaboratorInterestsWidgetDto, interestsDtos);
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="attendeeCollaboratorInterestsWidgetDto">The attendee collaborator interests widget dto.</param>
        /// <param name="interestsDtos">The interests.</param>
        public void UpdateBaseProperties(
            AttendeeCollaboratorInterestsWidgetDto attendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            this.UpdateInterests(attendeeCollaboratorInterestsWidgetDto, interestsDtos);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="interestsDtos">The interests.</param>
        public void UpdateDropdownProperties(
            List<InterestDto> interestsDtos)
        {
            this.UpdateInterests(null, interestsDtos);
        }

        /// <summary>
        /// Updates the interests.
        /// </summary>
        /// <param name="attendeeCollaboratorInterestsWidgetDto">The attendee collaborator interests widget dto.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <exception cref="System.ArgumentNullException">attendeeCollaboratorInterestsWidgetDto</exception>
        private void UpdateInterests(
            AttendeeCollaboratorInterestsWidgetDto attendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var attendeeCollaboratorInterestDto = attendeeCollaboratorInterestsWidgetDto?.AttendeeCollaboratorInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                interestsBaseCommands.Add(attendeeCollaboratorInterestDto != null ? new InterestBaseCommand(attendeeCollaboratorInterestDto) :
                    new InterestBaseCommand(interestDto));
            }

            var groupedInterestsDtos = interestsBaseCommands?
                .GroupBy(i => new { i.InterestGroupUid, i.InterestGroupName, i.InterestGroupDisplayOrder })?
                .OrderBy(g => g.Key.InterestGroupDisplayOrder)?
                .ToList();

            if (groupedInterestsDtos?.Any() == true)
            {
                this.Interests = new InterestBaseCommand[groupedInterestsDtos.Count][];
                for (int i = 0; i < groupedInterestsDtos.Count; i++)
                {
                    this.Interests[i] = groupedInterestsDtos[i].ToArray();
                }
            }
        }
    }
}