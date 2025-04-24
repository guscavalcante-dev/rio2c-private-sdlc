// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-22-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-22-2025
// ***********************************************************************
// <copyright file="INegotiationService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Services.Common;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.Interfaces
{
    /// <summary>INegotiationService</summary>
    public interface INegotiationService
    {
        /// <summary>
        /// Gets the common Availabilities configured between Player Executives and Producer Executives.
        /// Exclude dates that are not in common for both!
        /// </summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns>A list containing common dates between Player Executives and Producer Executives</returns>
        List<ExecutiveAvailability> GetExecutivesAvailabilities(ProjectBuyerEvaluation projectBuyerEvaluation);

        /// <summary>
        /// Gets only the player executives availabilities.
        /// </summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        List<ExecutiveAvailability> GetPlayerExecutivesAvailabilities(ProjectBuyerEvaluation projectBuyerEvaluation);

        /// <summary>
        /// Gets only the producer executives availabilities.
        /// </summary>
        /// <param name="projectBuyerEvaluation">The project buyer evaluation.</param>
        /// <returns></returns>
        List<ExecutiveAvailability> GetProducerExecutivesAvailabilities(ProjectBuyerEvaluation projectBuyerEvaluation);
    }
}