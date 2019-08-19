// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="CreateOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Statics;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateOrganization</summary>
    public class CreateOrganization : OrganizationBaseCommand
    {
        public OrganizationType OrganizationType { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateOrganization(List<HoldingBaseDto> holdingBaseDtos, List<LanguageDto> languagesDtos)
        {
            this.CropperImage = new CropperImageBaseCommand(null, null, FileRepositoryPathType.OrganizationImage);
            this.UpdateDescriptions(languagesDtos);
            this.UpdateBaseProperties(holdingBaseDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        public CreateOrganization()
        {
        }

        /// <summary>Updates the properties.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        public void UpdateProperties(List<HoldingBaseDto> holdingBaseDtos)
        {
            this.UpdateBaseProperties(holdingBaseDtos);
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateDescriptions(List<LanguageDto> languagesDtos)
        {
            this.Descriptions = new List<OrganizationDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Descriptions.Add(new OrganizationDescriptionBaseCommand(languageDto));
            }
        }
    }
}