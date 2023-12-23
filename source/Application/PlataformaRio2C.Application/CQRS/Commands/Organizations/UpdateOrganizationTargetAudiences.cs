// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="UpdateOrganizationTargetAudiences.cs" company="Softo">
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
    /// <summary>UpdateOrganizationTargetAudiences</summary>
    public class UpdateOrganizationTargetAudiences : BaseCommand
    {
        public Guid OrganizationUid { get; set; }
        public int ProjectTypeId { get; set; }

        [Display(Name = "TargetAudiences", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<OrganizationTargetAudienceBaseCommand> OrganizationTargetAudiences { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationTargetAudiences"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public UpdateOrganizationTargetAudiences(
            AttendeeOrganizationSiteTargetAudienceWidgetDto entity,
            List<TargetAudience> targetAudiences)
        {
            this.OrganizationUid = entity.Organization.Uid;
            this.UpdateTargetAudiences(entity, targetAudiences);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationTargetAudiences"/> class.</summary>
        public UpdateOrganizationTargetAudiences()
        {
        }

        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateTargetAudiences(AttendeeOrganizationSiteTargetAudienceWidgetDto entity, List<TargetAudience> targetAudiences)
        {
            this.OrganizationTargetAudiences = new List<OrganizationTargetAudienceBaseCommand>();
            foreach (var targetAudience in targetAudiences)
            {
                var organizationTargetAudience = entity?.OrganizationTargetAudiencesDtos?.FirstOrDefault(ota => ota.TargetAudienceUid == targetAudience.Uid);
                this.OrganizationTargetAudiences.Add(organizationTargetAudience != null ? new OrganizationTargetAudienceBaseCommand(organizationTargetAudience) :
                                                                                          new OrganizationTargetAudienceBaseCommand(targetAudience));
            }
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            int projectTypeId,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.ProjectTypeId = projectTypeId;
            base.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}