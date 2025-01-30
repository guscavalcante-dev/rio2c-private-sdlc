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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateMusicBusinessRoundProjectMainInformation</summary>
    public class UpdateMusicBusinessRoundProjectMainInformation : MusicBusinessRoundProjectBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectMainInformation"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="isProductionPlanRequired">if set to <c>true</c> [is production plan required].</param>
        /// <param name="isAdditionalInformationRequired">if set to <c>true</c> [is additional information required].</param>
        /// <param name="userInterfaceLanguage">if set to <c>true</c> [is additional information required].</param>
        /// <param name="projectModalityUid">The project modality uid.</param>
        public UpdateMusicBusinessRoundProjectMainInformation(
            MusicBusinessRoundProjectDto entity,
            List<LanguageDto> languagesDtos,
            bool isDataRequired,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage
        )
        {
            this.MusicProjectUid = entity?.Uid;
            this.AttachmentUrl = entity?.AttachmentUrl;
            this.UpdateBaseProperties(
                entity,
                languagesDtos,
                isDataRequired,
                userId,
                userUid,
                editionId,
                editionUid,
                userInterfaceLanguage
            );
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateMusicBusinessRoundProjectMainInformation"/> class.</summary>
        public UpdateMusicBusinessRoundProjectMainInformation()
        {
        }
    }
}