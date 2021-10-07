// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-06-2021
// ***********************************************************************
// <copyright file="UpdateOrganizationApiConfiguration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganizationApiConfiguration</summary>
    public class UpdateOrganizationApiConfiguration : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        [Display(Name = "DisplayOnSite", ResourceType = typeof(Labels))]
        public bool IsApiDisplayEnabled { get; set; }

        [Display(Name = "HighlightPosition", ResourceType = typeof(Labels))]
        public int? ApiHighlightPosition { get; set; }

        public Guid OrganizationTypeUid { get; set; }

        public int[] ApiHighlightPositions = new[] { 1, 2, 3, 4, 5 };
        public List<AttendeeOrganizationApiConfigurationWidgetDto> AttendeeOrganizationApiConfigurationWidgetDtos {  get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationApiConfiguration"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="organizationTypeUid">Name of the organization type.</param>
        /// <param name="attendeeOrganizationApiConfigurationWidgetDtos">The attendee organization API configuration widget dtos.</param>
        public UpdateOrganizationApiConfiguration(
            AttendeeOrganizationApiConfigurationWidgetDto entity, 
            Guid organizationTypeUid,
            List<AttendeeOrganizationApiConfigurationWidgetDto> attendeeOrganizationApiConfigurationWidgetDtos)
        {
            var attendeeOrganizationTypeDto = entity?.GetAttendeeOrganizationTypeDtoByOrganizationTypeUid(organizationTypeUid);

            this.OrganizationUid = entity?.Organization?.Uid ?? Guid.Empty;
            this.IsApiDisplayEnabled = attendeeOrganizationTypeDto?.AttendeeOrganizationType?.IsApiDisplayEnabled ?? false;
            this.ApiHighlightPosition = attendeeOrganizationTypeDto?.AttendeeOrganizationType?.ApiHighlightPosition;
            this.OrganizationTypeUid = organizationTypeUid;

            this.UpdateBaseModels(attendeeOrganizationApiConfigurationWidgetDtos, organizationTypeUid);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationApiConfiguration"/> class.</summary>
        public UpdateOrganizationApiConfiguration()
        {
        }

        /// <summary>Updates the base models.</summary>
        /// <param name="attendeeOrganizationApiConfigurationWidgetDtos">The attendee organization API configuration widget dtos.</param>
        public void UpdateBaseModels(List<AttendeeOrganizationApiConfigurationWidgetDto> attendeeOrganizationApiConfigurationWidgetDtos, Guid organizationTypeUid)
        {
            this.AttendeeOrganizationApiConfigurationWidgetDtos = attendeeOrganizationApiConfigurationWidgetDtos?
                                                                            .OrderBy(a => a.GetAttendeeOrganizationTypeDtoByOrganizationTypeUid(organizationTypeUid)
                                                                                                .AttendeeOrganizationType.ApiHighlightPosition)?
                                                                                                .ToList();
        }
    }
}