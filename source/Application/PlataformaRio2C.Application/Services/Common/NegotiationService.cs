// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-22-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-22-2025
// ***********************************************************************
// <copyright file="NegotiationService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Interfaces;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.Services.Common
{
    /// <summary>
    /// NegotiationService
    /// </summary>
    public class NegotiationService : INegotiationService
    {
        public NegotiationService() { }

        /// <summary>
        /// Gets the Availabilities configured into Player Executives and Producer Executives (if have), and calculate the common dates betweeen this Executives.
        /// </summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns>A list containing common dates between Player Executives and Producer Executives</returns>
        public List<ExecutiveAvailability> GetExecutivesAvailabilities(ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var playerAvailabilities = this.GetPlayerExecutivesAvailabilities(projectBuyerEvaluation);
            var producerAvailabilities = this.GetProducerExecutivesAvailabilities(projectBuyerEvaluation);

            // If there are no player availability, return the producer's availability (and vice versa)
            if (!playerAvailabilities.Any())
                return producerAvailabilities;
            if (!producerAvailabilities.Any())
                return playerAvailabilities;

            // Find the intersection between availabilities
            var intersectingAvailabilities = new List<ExecutiveAvailability>();
            foreach (var playerAvailability in playerAvailabilities)
            {
                foreach (var producerAvailability in producerAvailabilities)
                {
                    // Find the overlap between the two ranges
                    var overlapStart = playerAvailability.AvailabilityBeginDate > producerAvailability.AvailabilityBeginDate
                        ? playerAvailability.AvailabilityBeginDate
                        : producerAvailability.AvailabilityBeginDate;

                    var overlapEnd = playerAvailability.AvailabilityEndDate < producerAvailability.AvailabilityEndDate
                        ? playerAvailability.AvailabilityEndDate
                        : producerAvailability.AvailabilityEndDate;

                    // Check if have a valid overlap
                    if (overlapStart <= overlapEnd)
                    {
                        intersectingAvailabilities.Add(new ExecutiveAvailability(overlapStart, overlapEnd));
                    }
                }
            }

            return intersectingAvailabilities;
        }

        /// <summary>
        /// Gets the player executives availabilities.
        /// </summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        public List<ExecutiveAvailability> GetPlayerExecutivesAvailabilities(ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var playerExecutivesAvailabilities = projectBuyerEvaluation.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                    .Where(aoc => aoc.AttendeeCollaborator.AvailabilityBeginDate != null
                                                                    && aoc.AttendeeCollaborator.AvailabilityEndDate != null)
                                                    .Select(aoc => new ExecutiveAvailability(
                                                                    aoc.AttendeeCollaborator.AvailabilityBeginDate?.ToBrazilTimeZone().Date,
                                                                    aoc.AttendeeCollaborator.AvailabilityEndDate?.ToBrazilTimeZone().Date.AddDays(1).AddTicks(-1)))
                                                    .ToList();

            return playerExecutivesAvailabilities;
        }

        /// <summary>
        /// Gets the producer executives availabilities.
        /// </summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        public List<ExecutiveAvailability> GetProducerExecutivesAvailabilities(ProjectBuyerEvaluation projectBuyerEvaluation)
        {
            var producerExecutivesAvailabilities = projectBuyerEvaluation.Project.SellerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                    .Where(aoc => aoc.AttendeeCollaborator.AvailabilityBeginDate != null
                                                                    && aoc.AttendeeCollaborator.AvailabilityEndDate != null)
                                                    .Select(aoc => new ExecutiveAvailability(
                                                                    aoc.AttendeeCollaborator.AvailabilityBeginDate?.ToBrazilTimeZone().Date,
                                                                    aoc.AttendeeCollaborator.AvailabilityEndDate?.ToBrazilTimeZone().Date.AddDays(1).AddTicks(-1)))
                                                    .ToList();

            return producerExecutivesAvailabilities;
        }
    }

    /// <summary>
    /// ExecutiveAvailability
    /// </summary>
    public class ExecutiveAvailability
    {
        public DateTimeOffset? AvailabilityBeginDate { get; set; }
        public DateTimeOffset? AvailabilityEndDate { get; set; }

        public ExecutiveAvailability(DateTimeOffset? availabilityBeginDate, DateTimeOffset? availabilityEndDate)
        {
            AvailabilityBeginDate = availabilityBeginDate;
            AvailabilityEndDate = availabilityEndDate;
        }
    }
}
