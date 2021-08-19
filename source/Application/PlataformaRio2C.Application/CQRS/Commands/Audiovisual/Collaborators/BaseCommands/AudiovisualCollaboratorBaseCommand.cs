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
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public InterestBaseCommand[][] Interests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudiovisualCollaboratorBaseCommand" /> class.
        /// </summary>
        public AudiovisualCollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="commissionAttendeeCollaboratorInterestsWidgetDto">The entity.</param>
        /// <param name="interestsDtos">The interests.</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity,
            CommissionAttendeeCollaboratorInterestsWidgetDto commissionAttendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            base.UpdateBaseProperties(entity);
            this.UpdateInterests(commissionAttendeeCollaboratorInterestsWidgetDto, interestsDtos);
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="interestsDtos">The interests.</param>
        public void UpdateBaseProperties(
            CommissionAttendeeCollaboratorInterestsWidgetDto commissionAttendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            this.UpdateInterests(commissionAttendeeCollaboratorInterestsWidgetDto, interestsDtos);
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
        /// Updates the audiovisual interests.
        /// </summary>
        /// <param name="commissionAttendeeCollaboratorInterestsWidgetDto">The entity.</param>
        /// <param name="interests">The interests.</param>
        private void UpdateInterests(
            CommissionAttendeeCollaboratorInterestsWidgetDto commissionAttendeeCollaboratorInterestsWidgetDto,
            List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var commissionAttendeeCollaboratorInterestDto = commissionAttendeeCollaboratorInterestsWidgetDto?.CommissionAttendeeCollaboratorInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                interestsBaseCommands.Add(commissionAttendeeCollaboratorInterestDto != null ? new InterestBaseCommand(commissionAttendeeCollaboratorInterestDto) :
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