// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 01-30-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-30-2025
// ***********************************************************************
// <copyright file="UpdateMusicBusinessRoundProjectOptions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateMusicBusinessRoundProjectOptions</summary>
    public class UpdateMusicBusinessRoundProjectOptions : MusicBusinessRoundProjectBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectOptions" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="isProductionPlanRequired">if set to <c>true</c> [is production plan required].</param>
        /// <param name="isAdditionalInformationRequired">if set to <c>true</c> [is additional information required].</param>
        /// <param name="userInterfaceLanguage">if set to <c>true</c> [is additional information required].</param>
        public UpdateMusicBusinessRoundProjectOptions(
            MusicBusinessRoundProjectDto entity,
            List<LanguageDto> languagesDtos,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            List<Activity> activities,
            List<PlayerCategory> playersCategories,
            bool isDataRequired,
            string userInterfaceLanguage)
        {
            this.MusicProjectUid = entity?.Uid;
            this.AttachmentUrl = entity?.AttachmentUrl;
            this.UpdateBaseProperties(
                entity,
                languagesDtos,
                targetAudiences,
                interestsDtos,
                activities,
                playersCategories,
                isDataRequired,
                userInterfaceLanguage
            );
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectOptions"/> class.</summary>
        public UpdateMusicBusinessRoundProjectOptions()
        {
        }
    }
}