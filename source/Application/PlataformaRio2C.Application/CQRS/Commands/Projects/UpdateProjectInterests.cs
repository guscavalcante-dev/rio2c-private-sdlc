// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-21-2021
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

        public InterestBaseCommand[][] Interests { get; set; }

        public List<Guid> TargetAudiencesUids { get; set; }

        public List<TargetAudience> TargetAudiences { get; private set; }

        public Guid? AttendeeOrganizationUid { get; private set; }
        public Guid ProjectTypeUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectInterests"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public UpdateProjectInterests(
            ProjectDto entity,
            List<InterestDto> interestsDtos,
            List<TargetAudience> targetAudiences)
        {
            this.UpdateInterests(entity, interestsDtos);
            this.TargetAudiencesUids = entity?.ProjectTargetAudienceDtos?.Select(pta => pta.TargetAudience.Uid)?.ToList();

            this.UpdateDropdownProperties(targetAudiences);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectInterests"/> class.</summary>
        public UpdateProjectInterests()
        {
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateDropdownProperties(List<TargetAudience> targetAudiences)
        {
            this.TargetAudiences = targetAudiences;
        }

        #region Private methods

        /// <summary>Updates the interests.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateInterests(ProjectDto entity, List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var projectInterest = entity?.ProjectInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                interestsBaseCommands.Add(projectInterest != null ? new InterestBaseCommand(projectInterest) :
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

        #endregion
    }
}