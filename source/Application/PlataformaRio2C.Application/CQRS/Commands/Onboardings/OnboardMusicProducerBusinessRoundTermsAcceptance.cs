// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 01-08-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-08-2025
// ***********************************************************************
// <copyright file="OnboardAudiovisualProducerBusinessRoundTerms.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardAudiovisualProducerBusinessRoundTerms</summary>
    public class OnboardMusicProducerBusinessRoundTermsAcceptance : BaseCommand
    {
        [Range(typeof(bool), "true", "true", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ToProceedYouMustAcceptTheTerm")]
        public bool AcceptTerms { get; set; }

        public Guid? ProjectUid { get; set; }

        public Guid CollaboratorUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardMusicProducerBusinessRoundTermsAcceptance"/> class.</summary>
        public OnboardMusicProducerBusinessRoundTermsAcceptance(Guid? projectUid)
        {
            this.ProjectUid = projectUid;
            this.AcceptTerms = false;
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardMusicProducerBusinessRoundTermsAcceptance"/> class.</summary>
        public OnboardMusicProducerBusinessRoundTermsAcceptance()
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