// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-24-2019
// ***********************************************************************
// <copyright file="OrganizationBaseCommand.cs" company="Softo">
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
    /// <summary>OrganizationBaseCommand</summary>
    public class OrganizationBaseCommand : BaseCommand
    {
        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? HoldingUid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Name { get; set; }

        [Display(Name = "CompanyDocument", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "SocialMedia", ResourceType = typeof(Labels))]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SocialMedia { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        public AddressBaseCommand Address { get; set; }

        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public List<HoldingBaseDto> HoldingBaseDtos { get; private set; }
        public OrganizationType OrganizationType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationBaseCommand"/> class.</summary>
        public OrganizationBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public void UpdateBaseProperties(OrganizationDto entity, List<HoldingBaseDto> holdingBaseDtos, List<LanguageDto> languagesDtos, List<CountryBaseDto> countriesBaseDtos)
        {
            this.HoldingUid = entity?.HoldingBaseDto?.Uid;
            this.Name = entity?.Name;
            this.Document = entity?.Document;
            this.CompanyName = entity?.CompanyName;
            this.TradeName = entity?.TradeName;
            this.Website = entity?.Website;
            this.SocialMedia = entity?.SocialMedia;
            this.PhoneNumber = entity?.PhoneNumber;
            this.UpdateAddress(entity, countriesBaseDtos);
            this.UpdateDescriptions(entity, languagesDtos);
            this.UpdateCropperImage(entity);
            this.UpdateDropdownProperties(holdingBaseDtos, countriesBaseDtos);
        }

        /// <summary>Updates the address.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        private void UpdateAddress(OrganizationDto entity, List<CountryBaseDto> countriesBaseDtos)
        {
            this.Address = new AddressBaseCommand(entity?.AddressBaseDto, countriesBaseDtos);
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateDescriptions(OrganizationDto entity, List<LanguageDto> languagesDtos)
        {
            this.Descriptions = new List<OrganizationDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity?.DescriptionsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Descriptions.Add(description != null ? new OrganizationDescriptionBaseCommand(description) : 
                                                            new OrganizationDescriptionBaseCommand(languageDto));
            }
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        private void UpdateCropperImage(OrganizationDto entity)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.OrganizationImage);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public void UpdateDropdownProperties(List<HoldingBaseDto> holdingBaseDtos, List<CountryBaseDto> countriesBaseDtos)
        {
            this.HoldingBaseDtos = holdingBaseDtos;
            this.Address?.UpdateDropdownProperties(countriesBaseDtos);
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