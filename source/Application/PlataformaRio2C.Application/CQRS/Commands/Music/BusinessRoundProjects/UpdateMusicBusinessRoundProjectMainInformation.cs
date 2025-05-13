// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 01-30-2025
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 01-30-2025
// ***********************************************************************
// <copyright file="UpdateMusicBusinessRoundProjectMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateMusicBusinessRoundProjectMainInformation</summary>
    public class UpdateMusicBusinessRoundProjectMainInformation : MusicBusinessRoundProjectBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectMainInformation" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">if set to <c>true</c> [is additional information required].</param>
        public UpdateMusicBusinessRoundProjectMainInformation(
            MusicBusinessRoundProjectDto entity,
            List<LanguageDto> languagesDtos,
            bool isDataRequired,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.MusicProjectUid = entity?.Uid;
            this.AttachmentUrl = entity?.AttachmentUrl;
            this.UpdateBaseProperties(
                entity,
                languagesDtos,
                isDataRequired
            );
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectMainInformation"/> class.</summary>
        public UpdateMusicBusinessRoundProjectMainInformation()
        {
        }
    }
}