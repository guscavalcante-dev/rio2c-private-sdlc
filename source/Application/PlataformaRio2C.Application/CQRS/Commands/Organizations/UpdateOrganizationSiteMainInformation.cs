// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-22-2021
// ***********************************************************************
// <copyright file="UpdateOrganizationSiteMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganizationMainInformation</summary>
    public class UpdateOrganizationSiteMainInformation : UpdateOrganizationMainInformationBaseCommand
    {
        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationSiteMainInformation"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public UpdateOrganizationSiteMainInformation(
            AttendeeOrganizationMainInformationWidgetDto entity,
            List<LanguageDto> languagesDtos,
            bool isDescriptionRequired,
            bool isImageRequired)
            : base(entity, languagesDtos, isDescriptionRequired, isImageRequired)
        {
            this.TradeName = entity?.Organization?.TradeName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationSiteMainInformation"/> class.
        /// </summary>
        public UpdateOrganizationSiteMainInformation()
        {
        }
    }
}