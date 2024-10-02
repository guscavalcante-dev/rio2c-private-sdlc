// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 27-09-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 30-09-2024
// ***********************************************************************
// <copyright file="UpdateConferenceApiConfiguration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateConferenceApiConfiguration</summary>
    public class UpdateConferenceApiConfiguration : BaseCommand
    {
        public Guid ConferenceUid { get; set; }

        [Display(Name = "DisplayOnSite", ResourceType = typeof(Labels))]
        public bool IsApiDisplayEnabled { get; set; }

        [Display(Name = "HighlightPosition", ResourceType = typeof(Labels))]
        public string ApiHighlightPosition { get; set; }

        public int[] ApiHighlightPositions { get; private set; }

        public List<ConferenceDto> ConferencesApiConfigurationWidgetDtos { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceApiConfiguration"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attendeeCollaboratorApiConfigurationWidgetDtos">The attendee collaborator API configuration widget dtos.</param>
        public UpdateConferenceApiConfiguration(
            ConferenceDto entity,
            List<ConferenceDto> conferencesApiConfigurationWidgetDtos,
            int conferencesApiHighlightPositionsCount
        )
        {
            this.ConferenceUid = entity?.Conference?.Uid ?? Guid.Empty;
            this.IsApiDisplayEnabled = entity?.Conference?.IsApiDisplayEnabled ?? false;
            this.ApiHighlightPosition = entity?.Conference?.ApiHighlightPosition;
            this.UpdateBaseModels(conferencesApiConfigurationWidgetDtos);
            this.GenerateCountConferencesApiHighlightPositions(conferencesApiHighlightPositionsCount);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceApiConfiguration"/> class.</summary>
        public UpdateConferenceApiConfiguration()
        {
            //
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            int userId,
            Guid userUid,
            int editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            base.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>Updates the base models.</summary>
        /// <param name="conferencesApiConfigurationWidgetDtos">The conference API configuration widget dtos.</param>
        public void UpdateBaseModels(List<ConferenceDto> conferencesApiConfigurationWidgetDtos)
        {
            this.ConferencesApiConfigurationWidgetDtos = conferencesApiConfigurationWidgetDtos
                .OrderBy(c => c.Conference?.ApiHighlightPosition)
                .ToList();
        }

        /// <summary>Generates the count conferences API highlight positions.</summary>
        /// <param name="conferencesApiHighlightPositionsCount">The conferences API highlight positions count.</param>
        public void GenerateCountConferencesApiHighlightPositions(int conferencesApiHighlightPositionsCount)
        {
            this.ApiHighlightPositions = Enumerable.Range(1, conferencesApiHighlightPositionsCount).ToArray();
        }
    }
}