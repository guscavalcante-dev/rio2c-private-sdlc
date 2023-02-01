// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-13-2019
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

        /// <summary>Initializes a new instance of the <see cref="UpdateTinyCollaborator"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddingToCurrentEdition">The is adding to current edition.</param>
        public UpdateTinyCollaborator(
            CollaboratorDto entity, 
            bool? isAddingToCurrentEdition)
        {
            if (entity == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM));
            }

            this.CollaboratorUid = entity.Uid;
            this.IsAddingToCurrentEdition = isAddingToCurrentEdition ?? false;

            this.UpdateBaseProperties(entity);
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateTinyCollaborator"/> class.</summary>
        public UpdateTinyCollaborator()
        {
        }       
    }
}