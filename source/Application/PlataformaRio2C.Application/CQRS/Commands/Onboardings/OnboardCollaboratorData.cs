// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="OnboardCollaboratorData.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardCollaboratorData</summary>
    public class OnboardCollaboratorData : BaseCommand
    {
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailISInvalid")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.EmailAddress)]
        public string PublicEmail { get; set; }

        public List<CollaboratorJobTitleBaseCommand> JobTitles { get; set; }
        public List<CollaboratorMiniBioBaseCommand> MiniBios { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public Guid CollaboratorUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardCollaboratorData"/> class.</summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        public OnboardCollaboratorData(CollaboratorDto collaborator, List<LanguageDto> languagesDtos, bool isJobTitleRequired, bool isMiniBioRequired)
        {
            this.PublicEmail = collaborator?.PublicEmail;
            this.UpdateJobTitles(collaborator, languagesDtos, isJobTitleRequired);
            this.UpdateMiniBios(collaborator, languagesDtos, isMiniBioRequired);
            this.UpdateCropperImage(collaborator);
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardCollaboratorData"/> class.</summary>
        public OnboardCollaboratorData()
        {
        }

        /// <summary>Updates the job titles.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isJobTitleRequired">if set to <c>true</c> [is job title required].</param>
        private void UpdateJobTitles(CollaboratorDto entity, List<LanguageDto> languagesDtos, bool isJobTitleRequired)
        {
            this.JobTitles = new List<CollaboratorJobTitleBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var jobTitle = entity?.JobTitlesDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.JobTitles.Add(jobTitle != null ? new CollaboratorJobTitleBaseCommand(jobTitle, isJobTitleRequired) :
                                                      new CollaboratorJobTitleBaseCommand(languageDto, isJobTitleRequired));
            }
        }

        /// <summary>Updates the mini bios.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isMiniBioRequired">if set to <c>true</c> [is mini bio required].</param>
        private void UpdateMiniBios(CollaboratorDto entity, List<LanguageDto> languagesDtos, bool isMiniBioRequired)
        {
            this.MiniBios = new List<CollaboratorMiniBioBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var miniBio = entity?.MiniBiosDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.MiniBios.Add(miniBio != null ? new CollaboratorMiniBioBaseCommand(miniBio, isMiniBioRequired) :
                                                    new CollaboratorMiniBioBaseCommand(languageDto, isMiniBioRequired));
            }
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        private void UpdateCropperImage(CollaboratorDto entity)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.UserImage);
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            Guid collaboratorUid,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorUid = collaboratorUid;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}