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
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

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
        /// <param name="targetAudiences">The target audience.</param>
        /// <param name="interestsDtos">The interests.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="playerCategories">The player categories.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="userInterfaceLanguage">if set to <c>true</c> [is additional information required].</param>
        public UpdateMusicBusinessRoundProjectOptions(
            MusicBusinessRoundProjectDto entity,
            List<LanguageDto> languagesDtos,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            List<Activity> activities,
            List<PlayerCategory> playerCategories,
            bool isDataRequired,
            string userInterfaceLanguage)
        {
            this.MusicProjectUid = entity?.Uid;
            this.UpdateBaseProperties(
                entity,
                languagesDtos,
                targetAudiences,
                interestsDtos,
                activities,
                playerCategories,
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