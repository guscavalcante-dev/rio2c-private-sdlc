// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
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
        public List<Guid> InterestsUids { get; set; }
        public List<OrganizationRestrictionSpecificsBaseCommand> RestrictionSpecifics { get; set; }

        public List<IGrouping<InterestGroup, Interest>> GroupedInterests { get; private set; }
        //public UserBaseDto UpdaterBaseDto { get; private set; }
        //public DateTime UpdateDate { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationInterests"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isRestrictionSpecificRequired">if set to <c>true</c> [is restriction specific required].</param>
        public UpdateOrganizationInterests(
            AttendeeOrganizationSiteInterestWidgetDto entity,
            List<IGrouping<InterestGroup, Interest>> groupedInterests, 
            List<LanguageDto> languagesDtos, 
            bool isRestrictionSpecificRequired)
        {
            this.OrganizationUid = entity.Organization.Uid;
            //this.UpdaterBaseDto = entity.UpdaterDto;
            //this.UpdateDate = entity.UpdateDate;
            this.UpdateRestrictionSpecifics(entity, languagesDtos, isRestrictionSpecificRequired);
            this.InterestsUids = entity?.OrganizationInterestDtos?.Select(oid => oid.InterestUid)?.ToList();
            this.UpdateModelsAndLists(groupedInterests);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationInterests"/> class.</summary>
        public UpdateOrganizationInterests()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="groupedInterests">The grouped interests.</param>
        public void UpdateModelsAndLists(List<IGrouping<InterestGroup, Interest>> groupedInterests)
        {
            this.GroupedInterests = groupedInterests;
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

        #endregion
    }
}