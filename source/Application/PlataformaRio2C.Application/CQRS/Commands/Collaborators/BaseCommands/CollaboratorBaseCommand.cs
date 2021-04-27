// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-12-2019
// ***********************************************************************
// <copyright file="CollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CollaboratorBaseCommand</summary>
    public class CollaboratorBaseCommand : BaseCommand
    {
        [Display(Name = "FirstName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string FirstName { get; set; }

        [Display(Name = "LastNames", ResourceType = typeof(Labels))]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string LastNames { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailISInvalid")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CellPhone { get; set; }

        [Display(Name = "Document", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        [Display(Name = "Profile", ResourceType = typeof(Labels))]
        public int? RoleId { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        [Display(Name = "CollaboratorType", ResourceType = typeof(Labels))]
        public string CollaboratorTypeName { get; set; }
        public IEnumerable<CollaboratorType> CollaboratorTypes { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorBaseCommand"/> class.</summary>
        public CollaboratorBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="entity">The entity.</param>
        public void UpdateBaseProperties(
            CollaboratorDto entity)
        {
            this.FirstName = entity?.FirstName;
            this.LastNames = entity?.LastNames;
            this.Email = entity?.Email;
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="email">The email.</param>
        public void UpdateBaseProperties(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string cellPhone,
            string document)
        {
            this.FirstName = firstName;
            this.LastNames = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CellPhone = cellPhone;
            this.Document = document;
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="collabboratorTypeName">Name of the collabborator type.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            string collabboratorTypeName,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.CollaboratorTypeName = collabboratorTypeName;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}