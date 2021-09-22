// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 09-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="DisassociateOrganizationCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DisassociateOrganizationCollaborator</summary>
    public class DisassociateOrganizationCollaborator : BaseCommand
    {
        [Display(Name = "Company", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? OrganizationUid { get; set; }

        [Display(Name = "Executive", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorUid { get; set; }

        [Display(Name = "CollaboratorType", ResourceType = typeof(Labels))]
        public string CollaboratorTypeName { get; set; }

        [Display(Name = "OrganizationType", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? OrganizationTypeUid { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisassociateOrganizationCollaborator"/> class.
        /// </summary>
        /// <param name="organizationUid">The attendee organization uid.</param>
        public DisassociateOrganizationCollaborator(Guid? organizationUid)
        {
            this.OrganizationUid = organizationUid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisassociateOrganizationCollaborator"/> class.
        /// </summary>
        public DisassociateOrganizationCollaborator()
        {
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="collabboratorTypeName">Name of the collabborator type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
           string collabboratorTypeName,
           int userId,
           Guid userUid,
           int? editionId,
           Guid? editionUid,
           string userInterfaceLanguage)
        {
            this.CollaboratorTypeName = collabboratorTypeName;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}