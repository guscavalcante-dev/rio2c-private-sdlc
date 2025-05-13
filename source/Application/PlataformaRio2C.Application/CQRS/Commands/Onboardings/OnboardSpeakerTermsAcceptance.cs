// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-13-2019
// ***********************************************************************
// <copyright file="OnboardSpeakerTermsAcceptance.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardSpeakerTermsAcceptance</summary>
    public class OnboardSpeakerTermsAcceptance : BaseCommand
    {
        [Range(typeof(bool), "true", "true", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ToProceedYouMustAcceptTheTerm")]
        public bool AcceptTerms { get; set; }

        public Guid CollaboratorUid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardSpeakerTermsAcceptance"/> class.</summary>
        public OnboardSpeakerTermsAcceptance()
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