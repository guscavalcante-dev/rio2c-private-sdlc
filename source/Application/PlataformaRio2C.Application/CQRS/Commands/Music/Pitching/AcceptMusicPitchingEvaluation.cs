// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 11-22-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 11-22-2024
// ***********************************************************************
// <copyright file="AcceptMusicPitchingEvaluation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>AcceptMusicPitchingEvaluation</summary>
    public class AcceptMusicPitchingEvaluation : BaseCommand
    {
        [Display(Name = "Project", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? MusicBandUid { get; set; }

        public MusicBand MusicBand { get; private set; }

        public MusicProjectDto MusicProjectDto { get; private set; }

        public UserAccessControlDto UserAccessControlDto { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AcceptMusicPitchingEvaluation"/> class.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        /// <param name="musicBandUid">The music project dto.</param>
        public AcceptMusicPitchingEvaluation(
            MusicProjectDto musicProjectDto,
            Guid musicBandUid,
            UserAccessControlDto userAccessControlDto
        )
        {
            this.MusicBandUid = musicBandUid;
            this.UserAccessControlDto = userAccessControlDto;
            this.UpdateModelsAndLists(musicProjectDto);
        }

        /// <summary>Initializes a new instance of the <see cref="AcceptMusicPitchingEvaluation"/> class.</summary>
        public AcceptMusicPitchingEvaluation()
        {
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="isAdmin">The is admin.</param>
        public void UpdatePreSendProperties(
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage,
            UserAccessControlDto userAccessControlDto,
            bool? isAdmin = false
        )
        {
            this.UserAccessControlDto = userAccessControlDto;
            base.UpdatePreSendProperties(
                userId,
                userUid,
                editionId,
                editionUid,
                userInterfaceLanguage,
                isAdmin.Value
            );
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="musicProjectDto">The music project dto.</param>
        public void UpdateModelsAndLists(MusicProjectDto musicProjectDto)
        {
            this.MusicProjectDto = musicProjectDto;
        }
    }
}