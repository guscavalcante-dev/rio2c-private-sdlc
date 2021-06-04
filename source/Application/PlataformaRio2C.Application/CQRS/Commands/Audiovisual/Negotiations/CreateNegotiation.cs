// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-23-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="CreateNegotiation.cs" company="Softo">
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
    /// <summary>CreateNegotiation</summary>
    public class CreateNegotiation : NegotiationBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNegotiation"/> class.
        /// </summary>
        /// <param name="projectBuyerEvaluationDto">The project buyer evaluation dto.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <exception cref="DomainException"></exception>
        public CreateNegotiation(
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
        /// Initializes a new instance of the <see cref="CreateNegotiation"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation.</param>
        public CreateNegotiation()
        {
        }
    }
}