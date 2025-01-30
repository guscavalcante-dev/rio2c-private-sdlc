// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-26-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2025
// ***********************************************************************
// <copyright file="AcceptMusicBusinessRoundProjectEvaluation.cs" company="Softo">
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
    /// <summary>AcceptMusicBusinessRoundProjectEvaluation</summary>
    public class AcceptMusicBusinessRoundProjectEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? MusicBusinessRoundProjectUid { get; set; }

        [Display(Name = "Company", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AttendeeOrganizationUid { get; set; }

        public int MaximumAvailableSlotsByPlayer { get; set; }
        public int PlayerAcceptedProjectsCount { get; set; }
        public bool IsProjectsApprovalLimitReached => this.PlayerAcceptedProjectsCount >= this.MaximumAvailableSlotsByPlayer;

        public MusicBusinessRoundProjectDto MusicBusinessRoundProjectDto { get; private set; }
        public dynamic AttendeeOrganizations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptMusicBusinessRoundProjectEvaluation" /> class.
        /// </summary>
        /// <param name="musicBusinessRoundProjectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        /// <param name="maximumAvailableSlotsByPlayer">The maximum available slots by player.</param>
        /// <param name="playerAcceptedProjectsCount">The player accepted projects count.</param>
        public AcceptMusicBusinessRoundProjectEvaluation(
            MusicBusinessRoundProjectDto musicBusinessRoundProjectDto, 
            List<AttendeeOrganization> currentUserOrganizations, 
            int maximumAvailableSlotsByPlayer,
            int playerAcceptedProjectsCount)
        {
            this.MusicBusinessRoundProjectUid = musicBusinessRoundProjectDto?.Uid;
            this.UpdateOrganizations(musicBusinessRoundProjectDto, currentUserOrganizations);
            this.MaximumAvailableSlotsByPlayer = maximumAvailableSlotsByPlayer;
            this.PlayerAcceptedProjectsCount = playerAcceptedProjectsCount;
        }

        /// <summary>Initializes a new instance of the <see cref="AcceptMusicBusinessRoundProjectEvaluation"/> class.</summary>
        public AcceptMusicBusinessRoundProjectEvaluation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="musicBusinessRoundProjectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        public void UpdateModelsAndLists(MusicBusinessRoundProjectDto musicBusinessRoundProjectDto, List<AttendeeOrganization> currentUserOrganizations)
        {
            this.UpdateOrganizations(musicBusinessRoundProjectDto, currentUserOrganizations);
        }

        #region Private Methods

        /// <summary>Updates the organizations.</summary>
        /// <param name="musicBusinessRoundProjectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        private void UpdateOrganizations(MusicBusinessRoundProjectDto musicBusinessRoundProjectDto, List<AttendeeOrganization> currentUserOrganizations)
        {
            this.MusicBusinessRoundProjectDto = musicBusinessRoundProjectDto;

            this.AttendeeOrganizations = musicBusinessRoundProjectDto?.MusicBusinessRoundProjectBuyerEvaluationDtos?
                                                        .Where(pbed => !pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.IsDeleted
                                                                       && !pbed.BuyerAttendeeOrganizationDto.Organization.IsDeleted
                                                                       && currentUserOrganizations?.Select(cuo => cuo.Id)?.Contains(pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Id) == true)?
                                                        .Select(pbed => new
                                                        {
                                                            Uid = pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Uid,
                                                            Name = pbed.BuyerAttendeeOrganizationDto.Organization.Name
                                                        })?
                                                        .ToList();

            if (Enumerable.Count(this.AttendeeOrganizations) == 1)
            {
                this.AttendeeOrganizationUid = Enumerable.FirstOrDefault(this.AttendeeOrganizations)?.Uid;
            }
        }

        #endregion
    }
}