// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-16-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-22-2021
// ***********************************************************************
// <copyright file="UpdateOrganizationSocialNetworks.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganizationSocialNetworks</summary>
    public class UpdateOrganizationSocialNetworks : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "LinkedIn")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Linkedin { get; set; }

        [Display(Name = "Twitter")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Twitter { get; set; }

        [Display(Name = "Instagram")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Instagram { get; set; }

        [Display(Name = "YouTube")]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Youtube { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationSocialNetworks"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public UpdateOrganizationSocialNetworks(AttendeeOrganizationSiteDetailsDto entity)
        {
            this.OrganizationUid = entity?.Organization?.Uid ?? Guid.Empty;
            this.Website = entity?.Organization?.Website;
            this.Linkedin = entity?.Organization?.Linkedin;
            this.Twitter = entity?.Organization?.Twitter;
            this.Instagram = entity?.Organization?.Instagram;
            this.Youtube = entity?.Organization?.Youtube;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationSocialNetworks"/> class.</summary>
        public UpdateOrganizationSocialNetworks()
        {
        }
    }
}