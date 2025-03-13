// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 06-04-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-04-2021
// ***********************************************************************
// <copyright file="ScheduleManualMusicBusinessRoundNegotiation.cs" company="Softo">
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
    /// <summary>ScheduleManualMusicBusinessRoundNegotiation</summary>
    public class ScheduleManualMusicBusinessRoundNegotiation : MusicBusinessRoundNegotiationBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleManualMusicBusinessRoundNegotiation"/> class.
        /// </summary>
        /// <param name="musicBusinessRoundProjectDto">The project buyer evaluation dto.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="DomainException"></exception>
        public ScheduleManualMusicBusinessRoundNegotiation(
            MusicBusinessRoundProjectBuyerEvaluationDto musicBusinessRoundProjectDto,
            string userInterfaceLanguage)
        {
            if (musicBusinessRoundProjectDto == null )
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Negotiation, Labels.FoundM));
            }

            this.UpdateBaseProperties(ProjectBuyerEvaluationDto, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleManualMusicBusinessRoundNegotiation"/> class.
        /// </summary>
        public ScheduleManualMusicBusinessRoundNegotiation()
        {
        }
    }
}