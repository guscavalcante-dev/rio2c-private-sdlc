// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-26-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2025
// ***********************************************************************
// <copyright file="RefuseMusicBusinessRoundProjectEvaluation.cs" company="Softo">
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
    /// <summary>RefuseMusicBusinessRoundProjectEvaluation</summary>
    public class RefuseMusicBusinessRoundProjectEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? MusicBusinessRoundProjectUid { get; set; }

        [Display(Name = "Company", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? AttendeeOrganizationUid { get; set; }

        [Display(Name = "Reason", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectEvaluationRefuseReasonUid { get; set; }

        public bool HasAdditionalInfo { get; set; }

        [Display(Name = "Reason", ResourceType = typeof(Labels))]
        //[RequiredIf("HasAdditionalInfo", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(500, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Reason { get; set; }

        public MusicBusinessRoundProjectDto MusicBusinessRoundProjectDto { get; private set; }
        public dynamic AttendeeOrganizations { get; set; }
        public List<ProjectEvaluationRefuseReason> ProjectEvaluationRefuseReasons { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RefuseProjectEvaluation" /> class.
        /// </summary>
        /// <param name="musicBusinessRoundProjectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        /// <param name="projectEvaluationRefuseReasons">The project evaluation refuse reasons.</param>
        public RefuseMusicBusinessRoundProjectEvaluation(
            MusicBusinessRoundProjectDto musicBusinessRoundProjectDto,
            List<AttendeeOrganization> currentUserOrganizations,
            List<ProjectEvaluationRefuseReason> projectEvaluationRefuseReasons)
        {
            this.MusicBusinessRoundProjectUid = musicBusinessRoundProjectDto?.Uid;
            this.UpdateModelsAndLists(musicBusinessRoundProjectDto, currentUserOrganizations, projectEvaluationRefuseReasons);
            this.HasAdditionalInfo = false;
        }

        /// <summary>Initializes a new instance of the <see cref="AcceptProjectEvaluation"/> class.</summary>
        public RefuseMusicBusinessRoundProjectEvaluation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="musicBusinessRoundProjectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        /// <param name="projectEvaluationRefuseReasons">The project evaluation refuse reasons.</param>
        public void UpdateModelsAndLists(
            MusicBusinessRoundProjectDto musicBusinessRoundProjectDto,
            List<AttendeeOrganization> currentUserOrganizations,
            List<ProjectEvaluationRefuseReason> projectEvaluationRefuseReasons)
        {
            this.UpdateOrganizations(musicBusinessRoundProjectDto, currentUserOrganizations);
            this.ProjectEvaluationRefuseReasons = projectEvaluationRefuseReasons;
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