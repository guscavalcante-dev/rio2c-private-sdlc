﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-03-2024
// ***********************************************************************
// <copyright file="UpdateCollaboratorApiConfiguration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateCollaboratorApiConfiguration</summary>
    public class UpdateCollaboratorApiConfiguration : BaseCommand
    {
        public Guid CollaboratorUid { get; set; }

        [Display(Name = "DisplayOnSite", ResourceType = typeof(Labels))]
        public bool IsApiDisplayEnabled { get; set; }

        [Display(Name = "HighlightPosition", ResourceType = typeof(Labels))]
        public int? ApiHighlightPosition { get; set; }

        public string CollaboratorTypeName { get; private set; }

        public int[] ApiHighlightPositions { get; private set; }

        public List<AttendeeCollaboratorApiConfigurationWidgetDto> AttendeeCollaboratorApiConfigurationWidgetDtos { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorApiConfiguration"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="attendeeCollaboratorApiConfigurationWidgetDtos">The attendee collaborator API configuration widget dtos.</param>
        public UpdateCollaboratorApiConfiguration(
            AttendeeCollaboratorApiConfigurationWidgetDto entity,
            string collaboratorTypeName,
            List<AttendeeCollaboratorApiConfigurationWidgetDto> attendeeCollaboratorApiConfigurationWidgetDtos,
            int speakersApiHighlightPositionsCount)
        {
            var attendeeCollaboratorTypeDto = entity?.GetAttendeeCollaboratorTypeDtoByCollaboratorTypeName(collaboratorTypeName);

            this.CollaboratorUid = entity?.Collaborator?.Uid ?? Guid.Empty;
            this.IsApiDisplayEnabled = attendeeCollaboratorTypeDto?.AttendeeCollaboratorType?.IsApiDisplayEnabled ?? false;
            this.ApiHighlightPosition = attendeeCollaboratorTypeDto?.AttendeeCollaboratorType?.ApiHighlightPosition;

            this.UpdateBaseModels(attendeeCollaboratorApiConfigurationWidgetDtos);
            this.GenerateCountSpeakersApiHighlightPositions(speakersApiHighlightPositionsCount);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorApiConfiguration"/> class.</summary>
        public UpdateCollaboratorApiConfiguration()
        {
        }

        /// <summary>Updates the base models.</summary>
        /// <param name="attendeeCollaboratorApiConfigurationWidgetDtos">The attendee collaborator API configuration widget dtos.</param>
        public void UpdateBaseModels(List<AttendeeCollaboratorApiConfigurationWidgetDto> attendeeCollaboratorApiConfigurationWidgetDtos)
        {
            this.AttendeeCollaboratorApiConfigurationWidgetDtos = attendeeCollaboratorApiConfigurationWidgetDtos?
                                                                            .OrderBy(a => a.GetAttendeeCollaboratorTypeDtoByCollaboratorTypeName(
                                                                                Domain.Constants.CollaboratorType.Speaker).AttendeeCollaboratorType.ApiHighlightPosition)?
                                                                            .ToList();
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            string collaboratorTypeName,
            int userId,
            Guid userUid,
            int editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorTypeName = collaboratorTypeName;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>Generates the count speakers API highlight positions.</summary>
        /// <param name="speakersApiHighlightPositionsCount">The speakers API highlight positions count.</param>
        public void GenerateCountSpeakersApiHighlightPositions(int speakersApiHighlightPositionsCount)
        {
            this.ApiHighlightPositions = Enumerable.Range(1, speakersApiHighlightPositionsCount).ToArray();
        }
    }
}