// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="UpdateOrganizationMainInformationBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganizationMainInformationBaseCommand</summary>
    public class UpdateOrganizationMainInformationBaseCommand : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        public bool IsCompanyNameRequired { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [RequiredIf("IsCompanyNameRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

        public bool IsCompanyDocumentRequired { get; set; }

        [Display(Name = "CompanyDocument", ResourceType = typeof(Labels))]
        [RequiredIf("IsCompanyDocumentRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [ValidCompanyNumber]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public Country Country { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationMainInformationBaseCommand"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isCompanyNameRequired">if set to <c>true</c> [is company name required].</param>
        /// <param name="isCompanyDocumentRequired">if set to <c>true</c> [is company document required].</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public UpdateOrganizationMainInformationBaseCommand(
            AttendeeOrganizationMainInformationWidgetDto entity,
            List<LanguageDto> languagesDtos,
            bool isCompanyNameRequired,
            bool isCompanyDocumentRequired,
            bool isDescriptionRequired,
            bool isImageRequired)
        {
            this.OrganizationUid = entity.Organization.Uid;

            this.IsCompanyNameRequired = isCompanyNameRequired;
            this.CompanyName = entity?.Organization?.CompanyName;
            this.IsCompanyDocumentRequired = isCompanyDocumentRequired && entity?.Country?.IsCompanyNumberRequired == true;
            this.Document = entity?.Organization?.Document;

            this.UpdateDescriptions(entity, languagesDtos, isDescriptionRequired);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateModelsAndLists(entity.Country);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationMainInformationBaseCommand"/> class.
        /// </summary>
        public UpdateOrganizationMainInformationBaseCommand()
        {
        }

        public void UpdateModelsAndLists(Country country)
        {
            this.Country = country;
        }

        #region Private Methods

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        private void UpdateCropperImage(AttendeeOrganizationMainInformationWidgetDto entity, bool isImageRequired)
        {
            this.CropperImage = new CropperImageBaseCommand(entity.Organization.ImageUploadDate, entity.Organization.Uid, FileRepositoryPathType.OrganizationImage, isImageRequired);
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        private void UpdateDescriptions(AttendeeOrganizationMainInformationWidgetDto entity, List<LanguageDto> languagesDtos, bool isDescriptionRequired)
        {
            this.Descriptions = new List<OrganizationDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity?.DescriptionsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Descriptions.Add(description != null ? new OrganizationDescriptionBaseCommand(description, isDescriptionRequired) :
                                                            new OrganizationDescriptionBaseCommand(languageDto, isDescriptionRequired));
            }
        }

        #endregion
    }
}