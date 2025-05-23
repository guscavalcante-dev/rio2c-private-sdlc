﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="UpdateOrganizationActivities.cs" company="Softo">
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
    /// <summary>UpdateOrganizationActivities</summary>
    public class UpdateOrganizationActivities : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        public int? ProjectTypeId { get; set; }

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        public List<OrganizationActivityBaseCommand> OrganizationActivities { get; set; }

        public UpdateOrganizationActivities(
            AttendeeOrganizationSiteActivityWidgetDto entity,
            List<Activity> activities)
        {
            this.OrganizationUid = entity.Organization.Uid;
            this.UpdateActivities(entity, activities);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationActivities"/> class.</summary>
        public UpdateOrganizationActivities()
        {
        }

        #region Private Methods

        /// <summary>Updates the activities.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        private void UpdateActivities(AttendeeOrganizationSiteActivityWidgetDto entity, List<Activity> activities)
        {
            this.OrganizationActivities = new List<OrganizationActivityBaseCommand>();
            foreach (var activity in activities)
            {
                var organizationActivity = entity?.OrganizationActivitiesDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
                this.OrganizationActivities.Add(organizationActivity != null ? new OrganizationActivityBaseCommand(organizationActivity) :
                    new OrganizationActivityBaseCommand(activity));
            }
        }

        #endregion
    }
}