// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-11-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-11-2024
// ***********************************************************************
// <copyright file="OnboardAudiovisualPlayerTermsAcceptance.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardAudiovisualPlayerTermsAcceptance</summary>
    public class OnboardAudiovisualPlayerTermsAcceptance : BaseCommand
    {
        [Range(typeof(bool), "true", "true", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ToProceedYouMustAcceptTheTerm")]
        public bool AcceptTerms { get; set; }

        public Guid CollaboratorUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardAudiovisualPlayerTermsAcceptance"/> class.</summary>
        public OnboardAudiovisualPlayerTermsAcceptance()
        {
            this.AcceptTerms = false;
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