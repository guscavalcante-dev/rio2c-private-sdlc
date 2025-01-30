// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-09-2023
// ***********************************************************************
// <copyright file="UpdateTinyCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateTinyCollaborator</summary>
    public class UpdateTinyCollaborator : CollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public bool IsAddingToCurrentEdition { get; set; }
        public bool SkipUserEmailValidation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTinyCollaborator" /> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        /// <param name="skipUserEmailValidation">if set to <c>true</c> [skip user email validation].</param>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException"></exception>
        public UpdateTinyCollaborator(
            CollaboratorDto entity, 
            bool? isAddingToCurrentEdition,
            bool skipUserEmailValidation = false)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM));
            }

            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;
            this.SkipUserEmailValidation = skipUserEmailValidation;

            this.UpdateBaseProperties(entity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTinyCollaborator" /> class.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="document">The document.</param>
        /// <param name="address">The address.</param>
        /// <param name="country">The country.</param>
        /// <param name="state">The state.</param>
        /// <param name="city">The city.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="isUpdatingAddress">if set to <c>true</c> [is updating address].</param>
        public UpdateTinyCollaborator(
            Guid collaboratorUid,
            bool isAddingToCurrentEdition,
            string firstName,
            string lastNames,
            string stagename,
            string email,
            string phoneNumber,
            string cellPhone,
            string document,
            string address,
            string country,
            string state,
            string city,
            string zipCode,
            bool isUpdatingAddress)
        {
            this.CollaboratorUid = collaboratorUid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition;
            base.UpdateBaseProperties(
                firstName,
                lastNames,
                stagename,
                email,
                phoneNumber,
                cellPhone,
                document,
                address,
                country,
                state,
                city,
                zipCode,
                isUpdatingAddress);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateTinyCollaborator"/> class.</summary>
        public UpdateTinyCollaborator()
        {
        }       
    }
}