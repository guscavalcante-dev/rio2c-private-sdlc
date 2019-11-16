// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-16-2019
// ***********************************************************************
// <copyright file="UpdateProjectLinks.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateProjectLinks</summary>
    public class UpdateProjectLinks : BaseCommand
    {
        public Guid? ProjectUid { get; set; }

        [Display(Name = "LinksToImageOrConceptualLayout", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(3000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ImageLinks { get; set; }

        [Display(Name = "LinksForPromoTeaser", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(3000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TeaserLinks { get; set; }

        public Guid ProjectTypeUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectLinks"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public UpdateProjectLinks(ProjectDto entity)
        {
            this.ProjectUid = entity?.Project?.Uid;
            this.ImageLinks = entity?.ProjectImageLinkDtos?.FirstOrDefault()?.ProjectImageLink?.Value;
            this.TeaserLinks = entity?.ProjectTeaserLinkDtos?.FirstOrDefault()?.ProjectTeaserLink?.Value;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateProjectLinks"/> class.</summary>
        public UpdateProjectLinks()
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