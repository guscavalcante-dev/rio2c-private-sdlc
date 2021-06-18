// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-04-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-04-2021
// ***********************************************************************
// <copyright file="ScheduleManualNegotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ScheduleManualNegotiation</summary>
    public class ScheduleManualNegotiation : NegotiationBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleManualNegotiation"/> class.
        /// </summary>
        /// <param name="projectBuyerEvaluationDto">The project buyer evaluation dto.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="DomainException"></exception>
        public ScheduleManualNegotiation(
            ProjectBuyerEvaluationDto projectBuyerEvaluationDto,
            string userInterfaceLanguage)
        {
            if (projectBuyerEvaluationDto == null )
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM));
            }

            this.UpdateBaseProperties(projectBuyerEvaluationDto, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleManualNegotiation"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation.</param>
        public ScheduleManualNegotiation()
        {
        }
    }
}