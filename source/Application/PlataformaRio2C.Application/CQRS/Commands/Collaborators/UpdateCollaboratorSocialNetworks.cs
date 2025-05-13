// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-16-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-16-2023
// ***********************************************************************
// <copyright file="UpdateCollaboratorSocialNetworks.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateCollaboratorSocialNetworks</summary>
    public class UpdateCollaboratorSocialNetworks : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public string CollaboratorTypeName { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [Url(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UrlIsInvalid")]
        public string Website { get; set; }

        [Display(Name = "LinkedIn")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [Url(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UrlIsInvalid")]
        public string Linkedin { get; set; }

        [Display(Name = "Twitter")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [Url(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UrlIsInvalid")]
        public string Twitter { get; set; }

        [Display(Name = "Instagram")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [Url(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UrlIsInvalid")]
        public string Instagram { get; set; }

        [Display(Name = "YouTube")]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [Url(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "UrlIsInvalid")]
        public string Youtube { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCollaboratorSocialNetworks" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        public UpdateCollaboratorSocialNetworks(AttendeeCollaboratorSiteDetailsDto entity, string collaboratorTypeName)
        {
            this.Website = entity?.Collaborator?.Website;
            this.Linkedin = entity?.Collaborator?.Linkedin;
            this.Twitter = entity?.Collaborator?.Twitter;
            this.Instagram = entity?.Collaborator?.Instagram;
            this.Youtube = entity?.Collaborator?.Youtube;
            this.CollaboratorTypeName = collaboratorTypeName;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorSocialNetworks"/> class.</summary>
        public UpdateCollaboratorSocialNetworks()
        {
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            string collaboratorTypeName,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorTypeName = collaboratorTypeName;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}