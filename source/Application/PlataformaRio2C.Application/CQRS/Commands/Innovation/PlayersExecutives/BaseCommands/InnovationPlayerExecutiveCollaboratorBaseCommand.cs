// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 12-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-29-2023
// ***********************************************************************
// <copyright file="InnovationPlayerExecutiveCollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class InnovationPlayerExecutiveCollaboratorBaseCommand : CollaboratorBaseCommand
    {
        public List<AttendeeCollaboratorActivityBaseCommand> AttendeeCollaboratorActivities { get; set; }
        public List<AttendeeCollaboratorInterestBaseCommand> AttendeeCollaboratorInterests { get; set; }
        //public List<AttendeeCollaboratorInnovationOrganizationTrackBaseCommand> AttendeeCollaboratorInnovationOrganizationTracks { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationPlayerExecutiveCollaboratorBaseCommand" /> class.
        /// </summary>
        public InnovationPlayerExecutiveCollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity,
             List<AttendeeOrganizationBaseDto> attendeeOrganizationsBaseDtos,
            List<LanguageDto> languagesDtos,
            List<CollaboratorGender> genders,
            List<CollaboratorIndustry> industries,
            List<CollaboratorRole> collaboratorRoles,
            List<EditionDto> editionsDtos,
            List<Activity> activities,
            List<InterestDto> interestsDtos,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos,
            
            )
        {
            base.UpdateBaseProperties(entity);
            this.UpdateActivities(entity, activities);
            this.UpdateInterests(entity, interestsDtos);
            this.UpdateInnovationOrganizationTrackOptions(entity, innovationOrganizationTrackOptionDtos);
        }

        #region Privates

        /// <summary>
        /// Updates the activities.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        private void UpdateActivities(
            CollaboratorDto entity, 
            List<Activity> activities)
        {
            this.AttendeeCollaboratorActivities = new List<AttendeeCollaboratorActivityBaseCommand>();
            if (activities?.Any() == true)
            {
                foreach (var activity in activities)
                {
                    var attendeeCollaboratorActivityDto = entity?.AttendeeCollaboratorActivityDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
                    this.AttendeeCollaboratorActivities.Add(attendeeCollaboratorActivityDto != null ? new AttendeeCollaboratorActivityBaseCommand(attendeeCollaboratorActivityDto) :
                                                                                                      new AttendeeCollaboratorActivityBaseCommand(activity));
                }
            }
        }

        /// <summary>
        /// Updates the interests.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestDtos">The interest dtos.</param>
        private void UpdateInterests(
            CollaboratorDto entity, 
            List<InterestDto> interestDtos)
        {
            this.AttendeeCollaboratorInterests = new List<AttendeeCollaboratorInterestBaseCommand>();
            if (interestDtos?.Any() == true)
            {
                foreach (var interestDto in interestDtos)
                {
                    var attendeeCollaboratorInterestDto = entity?.AttendeeCollaboratorInterestDtos?.FirstOrDefault(oad => oad.InterestUid == interestDto.Interest.Uid);
                    this.AttendeeCollaboratorInterests.Add(attendeeCollaboratorInterestDto != null ? new AttendeeCollaboratorInterestBaseCommand(attendeeCollaboratorInterestDto) :
                                                                                                     new AttendeeCollaboratorInterestBaseCommand(interestDto));
                }
            }
        }

        /// <summary>
        /// Updates the innovation organization track options.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        private void UpdateInnovationOrganizationTrackOptions(
            CollaboratorDto entity, 
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            //this.AttendeeCollaboratorInnovationOrganizationTracks = new List<AttendeeCollaboratorInnovationOrganizationTrackBaseCommand>();
            //if (innovationOrganizationTrackOptionDtos?.Any() == true)
            //{
            //    foreach (var innovationOrganizationTrackOptionDto in innovationOrganizationTrackOptionDtos)
            //    {
            //        var attendeeCollaboratorInterestDto = entity?.AttendeeCollaboratorInnovationOrganizationTrackDtos?.FirstOrDefault(dto => dto.InnovationOrganizationTrackOption.Uid == innovationOrganizationTrackOptionDto.Uid);
            //        this.AttendeeCollaboratorInterests.Add(attendeeCollaboratorInterestDto != null ? new AttendeeCollaboratorInnovationOrganizationTrackBaseCommand(attendeeCollaboratorInterestDto) :
            //                                                                                         new AttendeeCollaboratorInnovationOrganizationTrackBaseCommand(innovationOrganizationTrackOptionDto));
            //    }
            //}
        }

        #endregion
    }
}