// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="AcceptProjectEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AcceptProjectEvaluation</summary>
    public class AcceptProjectEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectUid { get; set; }

        [Display(Name = "Company", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AttendeeOrganizationUid { get; set; }

        public int AvailableSlotsByPlayer { get; set; }
        public int PlayerAcceptedProjectsCount { get; set; }
        public bool IsProjectsApprovalLimitReached => this.PlayerAcceptedProjectsCount >= this.AvailableSlotsByPlayer;

        public ProjectDto ProjectDto { get; private set; }
        public dynamic AttendeeOrganizations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptProjectEvaluation" /> class.
        /// </summary>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="currentUserAttendeeOrganizations">The current user organizations.</param>
        /// <param name="availableSlotsByPlayer">The maximum available slots by player.</param>
        /// <param name="playerAcceptedProjectsCount">The player accepted projects count.</param>
        public AcceptProjectEvaluation(
            ProjectDto projectDto,
            List<AttendeeOrganization> currentUserAttendeeOrganizations,
            int availableSlotsByPlayer,
            int playerAcceptedProjectsCount)
        {
            this.ProjectUid = projectDto?.Project?.Uid;
            this.UpdateAttendeeOrganizations(projectDto, currentUserAttendeeOrganizations);
            this.AvailableSlotsByPlayer = availableSlotsByPlayer;
            this.PlayerAcceptedProjectsCount = playerAcceptedProjectsCount;
        }

        /// <summary>Initializes a new instance of the <see cref="AcceptProjectEvaluation"/> class.</summary>
        public AcceptProjectEvaluation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        public void UpdateModelsAndLists(ProjectDto projectDto, List<AttendeeOrganization> currentUserOrganizations)
        {
            this.UpdateAttendeeOrganizations(projectDto, currentUserOrganizations);
        }

        #region Private Methods

        /// <summary>Updates the organizations.</summary>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="currentUserAttendeeOrganizations">The current user organizations.</param>
        private void UpdateAttendeeOrganizations(ProjectDto projectDto, List<AttendeeOrganization> currentUserAttendeeOrganizations)
        {
            this.ProjectDto = projectDto;

            this.AttendeeOrganizations = projectDto?.ProjectBuyerEvaluationDtos?
                                                        .Where(pbed => !pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.IsDeleted
                                                                       && !pbed.BuyerAttendeeOrganizationDto.Organization.IsDeleted
                                                                       && currentUserAttendeeOrganizations?
                                                                            .Select(cuo => cuo.Id)?
                                                                            .Contains(pbed.BuyerAttendeeOrganizationDto.AttendeeOrganization.Id) == true)?
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