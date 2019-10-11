// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
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

        [Display(Name = "TargetAudiences", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<Guid> TargetAudiencesUids { get; set; }

        //public UserBaseDto UpdaterBaseDto { get; set; }
        //public DateTime UpdateDate { get; set; }

        public List<TargetAudience> TargetAudiences { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationTargetAudiences"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public UpdateOrganizationTargetAudiences(
            AttendeeOrganizationSiteTargetAudienceWidgetDto entity,
            List<TargetAudience> targetAudiences)
        {
            this.OrganizationUid = entity.Organization.Uid;
            this.TargetAudiencesUids = entity?.OrganizationTargetAudiencesDtos?.Select(otad => otad.TargetAudienceUid)?.ToList();
            this.UpdateModelsAndLists(targetAudiences);
            //this.UpdaterBaseDto = entity.UpdaterDto;
            //this.UpdateDate = entity.UpdateDate;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationTargetAudiences"/> class.</summary>
        public UpdateOrganizationTargetAudiences()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateModelsAndLists(List<TargetAudience> targetAudiences)
        {
            this.TargetAudiences = targetAudiences;
        }
    }
}