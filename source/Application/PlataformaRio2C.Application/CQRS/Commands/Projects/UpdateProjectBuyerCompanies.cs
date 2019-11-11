// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="UpdateProjectBuyerCompanies.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateProjectBuyerCompanies</summary>
    public class UpdateProjectBuyerCompanies : BaseCommand
    {
        public Guid? ProjectUid { get; set; }

        public Guid ProjectTypeUid { get; private set; }

        public UpdateProjectBuyerCompanies(ProjectDto entity)
        {
            this.ProjectUid = entity?.Project?.Uid;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectBuyerCompanies"/> class.</summary>
        public UpdateProjectBuyerCompanies()
        {
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