// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="OnboardPlayerInterests.cs" company="Softo">
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
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardPlayerInterests</summary>
    public class OnboardPlayerInterests : BaseCommand
    {
        public Guid OrganizationUid { get; set; }
        public List<Guid> InterestsUids { get; set; }

        public List<IGrouping<InterestGroup, Interest>> GroupedInterests { get; private set; }
        public UserBaseDto UpdaterBaseDto { get; private set; }
        public DateTime UpdateDate { get; private set; }

        // Pre send properties
        public OrganizationType OrganizationType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerInterests"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        public OnboardPlayerInterests(OrganizationDto entity, List<IGrouping<InterestGroup, Interest>> groupedInterests)
        {
            this.OrganizationUid = entity.Uid;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            this.InterestsUids = entity?.OrganizationInterestsDtos?.Select(oid => oid.InterestUid)?.ToList();
            this.UpdateDropdownProperties(groupedInterests);
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerInterests"/> class.</summary>
        public OnboardPlayerInterests()
        {
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="groupedInterests">The grouped interests.</param>
        public void UpdateDropdownProperties(List<IGrouping<InterestGroup, Interest>> groupedInterests)
        {
            this.GroupedInterests = groupedInterests;
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            OrganizationType organizationType,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.OrganizationType = organizationType;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}