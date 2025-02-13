// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="UpdateOrganizationInterests.cs" company="Softo">
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
    /// <summary>UpdateOrganizationInterests</summary>
    public class UpdateOrganizationInterests : BaseCommand
    {
        public Guid OrganizationUid { get; set; }
        
        public int? ProjectTypeId { get; set; }

        public List<OrganizationRestrictionSpecificsBaseCommand> RestrictionSpecifics { get; set; }
        public InterestBaseCommand[][] Interests { get; set; }

        public List<IGrouping<InterestGroup, Interest>> GroupedInterests { get; private set; }
        //public UserBaseDto UpdaterBaseDto { get; private set; }
        //public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationInterests" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isRestrictionSpecificRequired">if set to <c>true</c> [is restriction specific required].</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        public UpdateOrganizationInterests(
            AttendeeOrganizationSiteInterestWidgetDto entity,
            List<InterestDto> interestsDtos,
            List<LanguageDto> languagesDtos, 
            bool isRestrictionSpecificRequired, 
            int? projectTypeId)
        {
            this.OrganizationUid = entity.Organization.Uid;
            this.UpdateRestrictionSpecifics(entity, languagesDtos, isRestrictionSpecificRequired);
            this.UpdateInterests(entity, interestsDtos);
            this.ProjectTypeId = projectTypeId;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationInterests"/> class.</summary>
        public UpdateOrganizationInterests()
        {
        }

        #region Private Methods

        /// <summary>Updates the restriction specifics.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isRestrictionSpecificRequired">if set to <c>true</c> [is restriction specific required].</param>
        private void UpdateRestrictionSpecifics(AttendeeOrganizationSiteInterestWidgetDto entity, List<LanguageDto> languagesDtos, bool isRestrictionSpecificRequired)
        {
            this.RestrictionSpecifics = new List<OrganizationRestrictionSpecificsBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var restrictionSpecific = entity?.RestrictionSpecificDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.RestrictionSpecifics.Add(restrictionSpecific != null ? new OrganizationRestrictionSpecificsBaseCommand(restrictionSpecific, isRestrictionSpecificRequired) :
                    new OrganizationRestrictionSpecificsBaseCommand(languageDto, isRestrictionSpecificRequired));
            }
        }

        /// <summary>Updates the interests.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateInterests(AttendeeOrganizationSiteInterestWidgetDto entity, List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var organizationInterest = entity?.OrganizationInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                interestsBaseCommands.Add(organizationInterest != null ? new InterestBaseCommand(organizationInterest) :
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