// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-13-2025
// ***********************************************************************
// <copyright file="CreateAudiovisualBusinessRoundProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateProject</summary>
    public class CreateMusicBusinessRoundProject : MusicBusinessRoundProjectBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateMusicBusinessRoundProject"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="isProductionPlanRequired">if set to <c>true</c> [is production plan required].</param>
        /// <param name="isAdditionalInformationRequired">if set to <c>true</c> [is additional information required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateMusicBusinessRoundProject(
            MusicBusinessRoundProjectDto entity,
            List<LanguageDto> languagesDtos,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            bool isDataRequired,
            bool isProductionPlanRequired,
            bool isAdditionalInformationRequired,
            string userInterfaceLanguage)
        {
            this.UpdateBaseProperties(
                entity,
                languagesDtos,
                targetAudiences,
                interestsDtos,
                isDataRequired,
                isProductionPlanRequired,
                isAdditionalInformationRequired,
                userInterfaceLanguage,
                true
            );
        }

        /// <summary>Initializes a new instance of the <see cref="CreateMusicBusinessRoundProject"/> class.</summary>
        public CreateMusicBusinessRoundProject()
        {
        }
    }
}