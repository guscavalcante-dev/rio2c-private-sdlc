// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-21-2019
// ***********************************************************************
// <copyright file="UpdateOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganization</summary>
    public class UpdateOrganization : OrganizationBaseCommand
    {
        public Guid OrganizationUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }
        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganization"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <exception cref="DomainException"></exception>
        public UpdateOrganization(OrganizationDto entity, List<HoldingBaseDto> holdingBaseDtos, List<LanguageDto> languagesDtos, bool? isAddingToCurrentEdition)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Player, Labels.FoundM));
            }

            this.OrganizationUid = entity.Uid;
            this.HoldingUid = entity.HoldingBaseDto?.Uid;
            this.Name = entity.Name;
            this.Document = entity.Document;
            this.CompanyName = entity.CompanyName;
            this.TradeName = entity.TradeName;
            this.Website = entity.Website;
            this.SocialMedia = entity.SocialMedia;
            this.PhoneNumber = entity.PhoneNumber;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateDescriptions(entity, languagesDtos);
            this.CropperImage = new CropperImageBaseCommand(entity.ImageUploadDate, entity.Uid, FileRepositoryPathType.OrganizationImage);
            this.UpdateErrorProperties(holdingBaseDtos);
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganization"/> class.</summary>
        public UpdateOrganization()
        {
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateDescriptions(OrganizationDto entity, List<LanguageDto> languagesDtos)
        {
            this.Descriptions = new List<OrganizationDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity.DescriptionsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                if (description != null)
                {
                    this.Descriptions.Add(new OrganizationDescriptionBaseCommand(description));
                }
                else
                {
                    this.Descriptions.Add(new OrganizationDescriptionBaseCommand(languageDto));
                }
            }
        }
    }
}
