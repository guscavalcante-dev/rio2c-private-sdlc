// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="UpdateProjectInterests.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateProjectInterest</summary>
    public class UpdateProjectInterests : BaseCommand
    {
        public Guid? ProjectUid { get; set; }

        public List<Guid> InterestsUids { get; set; }
        public List<Guid> TargetAudiencesUids { get; set; }

        public List<IGrouping<InterestGroup, Interest>> GroupedInterests { get; private set; }
        public List<TargetAudience> TargetAudiences { get; private set; }

        public Guid? AttendeeOrganizationUid { get; private set; }
        public Guid ProjectTypeUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectInterests"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public UpdateProjectInterests(
            ProjectDto entity,
            List<IGrouping<InterestGroup, Interest>> groupedInterests,
            List<TargetAudience> targetAudiences)
        {
            this.InterestsUids = entity?.ProjectInterestDtos?.Select(pid => pid.Interest.Uid)?.ToList();
            this.TargetAudiencesUids = entity?.ProjectTargetAudienceDtos?.Select(pta => pta.TargetAudience.Uid)?.ToList();

            this.UpdateDropdownProperties(groupedInterests, targetAudiences);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectInterests"/> class.</summary>
        public UpdateProjectInterests()
        {
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="groupedInterests">The grouped interests.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateDropdownProperties(
            List<IGrouping<InterestGroup, Interest>> groupedInterests,
            List<TargetAudience> targetAudiences)
        {
            this.GroupedInterests = groupedInterests;
            this.TargetAudiences = targetAudiences;
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="projectTypeUid">The project type uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            Guid projectTypeUid,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.ProjectTypeUid = projectTypeUid;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}