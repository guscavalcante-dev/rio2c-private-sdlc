// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="RefuseProjectEvaluation.cs" company="Softo">
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
    /// <summary>RefuseProjectEvaluation</summary>
    public class RefuseProjectEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ProjectUid { get; set; }

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

        public ProjectDto ProjectDto { get; private set; }
        public dynamic AttendeeOrganizations { get; set; }
        public List<ProjectEvaluationRefuseReason> ProjectEvaluationRefuseReasons { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RefuseProjectEvaluation"/> class.</summary>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        /// <param name="projectEvaluationRefuseReasons">The project evaluation refuse reasons.</param>
        public RefuseProjectEvaluation(
            ProjectDto projectDto, 
            List<AttendeeOrganization> currentUserOrganizations, 
            List<ProjectEvaluationRefuseReason> projectEvaluationRefuseReasons)
        {
            this.ProjectUid = projectDto?.Project?.Uid;
            this.UpdateModelsAndLists(projectDto, currentUserOrganizations, projectEvaluationRefuseReasons);
            this.HasAdditionalInfo = false;
        }

        /// <summary>Initializes a new instance of the <see cref="AcceptProjectEvaluation"/> class.</summary>
        public RefuseProjectEvaluation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        /// <param name="projectEvaluationRefuseReasons">The project evaluation refuse reasons.</param>
        public void UpdateModelsAndLists(
            ProjectDto projectDto, 
            List<AttendeeOrganization> currentUserOrganizations,
            List<ProjectEvaluationRefuseReason> projectEvaluationRefuseReasons)
        {
            this.UpdateOrganizations(projectDto, currentUserOrganizations);
            this.ProjectEvaluationRefuseReasons = projectEvaluationRefuseReasons;
        }

        #region Private Methods

        /// <summary>Updates the organizations.</summary>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="currentUserOrganizations">The current user organizations.</param>
        private void UpdateOrganizations(ProjectDto projectDto, List<AttendeeOrganization> currentUserOrganizations)
        {
            this.ProjectDto = projectDto;

            this.AttendeeOrganizations = projectDto?.ProjectBuyerEvaluationDtos?
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