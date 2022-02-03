// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-02-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2022
// ***********************************************************************
// <copyright file="CartoonProjectOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CartoonProjectOrganization</summary>
    public class CartoonProjectOrganization : Entity
    {
        public static readonly int NameMinLength = 0;
        public static readonly int NameMaxLength = 300;

        public static readonly int TradeNameMinLength = 0;
        public static readonly int TradeNameMaxLength = 300;

        public static readonly int DocumentMinLength = 0;
        public static readonly int DocumentMaxLength = 50;

        public static readonly int PhoneNumberMinLength = 0;
        public static readonly int PhoneNumberMaxLength = 50;

        public static readonly int ReelUrlMinLength = 0;
        public static readonly int ReelUrlMaxLength = 100;

        public int CartoonProjectId { get; private set; }
        public int? AddressId { get; private set; }
        public string Name { get; private set; }
        public string TradeName { get; private set; }
        public string Document { get; private set; }
        public string PhoneNumber { get; private set; }
        public string ReelUrl { get; private set; }

        public virtual CartoonProject CartoonProject { get; private set; }
        public virtual Address Address { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProjectOrganization" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="reelUrl">The reel URL.</param>
        /// <param name="address">The address.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartoonProject">The cartoon project.</param>
        public CartoonProjectOrganization(
            string name,
            string tradeName,
            string document,
            string phoneNumber,
            string reelUrl,
            string address,
            string zipCode,
            int userId,
            CartoonProject cartoonProject)
        {
            this.Name = name;
            this.TradeName = tradeName;
            this.Document = document;
            this.PhoneNumber = phoneNumber;
            this.ReelUrl = reelUrl;
            this.CartoonProject = cartoonProject;

            this.UpdateAddress(address, zipCode, userId);
            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProjectOrganization" /> class.
        /// </summary>
        protected CartoonProjectOrganization()
        {
        }

        /// <summary>
        /// Updates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="reelUrl">The reel URL.</param>
        /// <param name="address">The address.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartoonProject">The cartoon project.</param>
        public void Update(
            string name,
            string tradeName,
            string document,
            string phoneNumber,
            string reelUrl,
            string address,
            string zipCode,
            int userId,
            CartoonProject cartoonProject)
        {
            this.Name = name;
            this.TradeName = tradeName;
            this.Document = document;
            this.PhoneNumber = phoneNumber;
            this.ReelUrl = reelUrl;
            this.CartoonProject = cartoonProject;

            this.UpdateAddress(address, zipCode, userId);
            base.SetUpdateDate(userId);
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            base.Delete(userId);
        }

        #region Address

        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="zipCode">The address zip code.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAddress(
            string address,
            string zipCode,
            int userId)
        {
            if (this.Address == null)
            {
                this.Address = new Address(
                    null,
                    null,
                    null,
                    null,
                    null,
                    address,
                    zipCode,
                    true,
                    userId);
            }
            else
            {
                this.Address.Update(
                    null,
                    null,
                    null,
                    null,
                    null,
                    address,
                    zipCode,
                    true,
                    userId);
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateMaxLengths();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the maximum lengths.
        /// </summary>
        public void ValidateMaxLengths()
        {
            if (!string.IsNullOrEmpty(this.Name) && this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }

            if (!string.IsNullOrEmpty(this.TradeName) && this.TradeName?.Trim().Length > TradeNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TradeName, TradeNameMaxLength, TradeNameMinLength), new string[] { "TradeName" }));
            }

            if (!string.IsNullOrEmpty(this.Document) && this.Document?.Trim().Length > DocumentMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Document, DocumentMaxLength, DocumentMinLength), new string[] { "Document" }));
            }

            if (!string.IsNullOrEmpty(this.PhoneNumber) && this.PhoneNumber?.Trim().Length > PhoneNumberMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.PhoneNumber, PhoneNumberMaxLength, PhoneNumberMinLength), new string[] { "PhoneNumber" }));
            }

            if (!string.IsNullOrEmpty(this.ReelUrl) && this.ReelUrl?.Trim().Length > ReelUrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ReelUrl, ReelUrlMaxLength, ReelUrlMinLength), new string[] { "ReelUrl" }));
            }
        }

        #endregion
    }
}

