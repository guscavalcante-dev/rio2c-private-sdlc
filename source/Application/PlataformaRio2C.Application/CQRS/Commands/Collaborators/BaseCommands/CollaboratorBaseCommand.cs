// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-08-2024
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

        #region CellPhone
        public bool IsUpdatingCellPhone { get; private set; } = false;

        [Display(Name = "CellPhone", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CellPhone { get; set; }

        #endregion

        [Display(Name = "Document", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        [Display(Name = "Profile", ResourceType = typeof(Labels))]
        public int? RoleId { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        [Display(Name = "CollaboratorType", ResourceType = typeof(Labels))]
        public string CollaboratorTypeName { get; set; }
        public IEnumerable<CollaboratorType> CollaboratorTypes { get; set; }

        #region Address

        public bool IsUpdatingAddress { get; private set; } = false;

        [Display(Name = "Address", ResourceType = typeof(Labels))]
        [StringLength(500, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Address { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Country { get; set; }

        [Display(Name = "State", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string State { get; set; }

        [Display(Name = "City", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string City { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string ZipCode { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorBaseCommand" /> class.
        /// </summary>
        public CollaboratorBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
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
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="document">The document.</param>
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

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="email">The email.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="document">The document.</param>
        /// <param name="isUpdatingCellPhone">if set to <c>true</c> [is updating cell phone].</param>
        public void UpdateBaseProperties(
            string firstName,
            string email,
            string cellPhone,
            string document,
            bool isUpdatingCellPhone)
        {
            this.FirstName = firstName;
            this.Email = email;
            this.CellPhone = cellPhone;
            this.Document = document;
            this.IsUpdatingCellPhone = isUpdatingCellPhone;
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
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
        public void UpdateBaseProperties(
            string firstName,
            string lastName,
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
            this.FirstName = firstName;
            this.LastNames = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CellPhone = cellPhone;
            this.Document = document;

            this.Address = address;
            this.Country = country;
            this.State = state;
            this.City = city;
            this.ZipCode = zipCode;
            this.IsUpdatingAddress = isUpdatingAddress;
        }

        /// <summary>
        /// Updates the pre send properties.
        /// </summary>
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
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}