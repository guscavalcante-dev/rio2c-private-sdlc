﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 12-06-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 12-06-2024
// ***********************************************************************
// <copyright file="OnboardAudiovisualProducerPitchingTerms.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardAudiovisualProducerPitchingTerms</summary>
    public class OnboardAudiovisualProducerPitchingTerms : BaseCommand
    {
        [Range(typeof(bool), "true", "true", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ToProceedYouMustAcceptTheTerm")]
        public bool AcceptTerms { get; set; }

        public Guid? ProjectUid { get; set; }

        public Guid CollaboratorUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardAudiovisualProducerPitchingTerms"/> class.</summary>
        public OnboardAudiovisualProducerPitchingTerms(Guid? projectUid)
        {
            this.ProjectUid = projectUid;
            this.AcceptTerms = false;
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardAudiovisualProducerPitchingTerms"/> class.</summary>
        public OnboardAudiovisualProducerPitchingTerms()
        {
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