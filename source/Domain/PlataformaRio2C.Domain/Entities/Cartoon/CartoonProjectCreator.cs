// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-02-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2022
// ***********************************************************************
// <copyright file="CartoonProjectCreator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CartoonProjectCreator</summary>
    public class CartoonProjectCreator : Entity
    {
        public static readonly int FirstNameMinLength = 1;
        public static readonly int FirstNameMaxLength = 300;

        public static readonly int LastNameMinLength = 1;
        public static readonly int LastNameMaxLength = 300;

        public static readonly int DocumentMinLength = 1;
        public static readonly int DocumentMaxLength = 50;

        public static readonly int EmailMinLength = 1;
        public static readonly int EmailMaxLength = 300;

        public static readonly int CellPhoneMinLength = 1;
        public static readonly int CellPhoneMaxLength = 50;

        public static readonly int PhoneNumberMinLength = 0;
        public static readonly int PhoneNumberMaxLength = 50;

        public static readonly int MiniBioMinLength = 1;
        public static readonly int MiniBioMaxLength = 3000;

        public int CartoonProjectId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string CellPhone { get; private set; }
        public string PhoneNumber { get; private set; }
        public string MiniBio { get; private set; }
        public bool IsResponsible { get; private set; }

        public virtual CartoonProject CartoonProject { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProjectCreator"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="document">The document.</param>
        /// <param name="email">The email.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="miniBio">The mini bio.</param>
        /// <param name="isResponsible">if set to <c>true</c> [is responsible].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartoonProject">The cartoon project.</param>
        public CartoonProjectCreator(
            string firstName,
            string lastName,
            string document,
            string email,
            string cellPhone,
            string phoneNumber,
            string miniBio,
            bool isResponsible,
            int userId,
            CartoonProject cartoonProject)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Document = document;
            this.Email = email;
            this.CellPhone = cellPhone;
            this.PhoneNumber = phoneNumber;
            this.MiniBio = miniBio;
            this.IsResponsible = isResponsible;
            this.CartoonProject = cartoonProject;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProjectCreator" /> class.
        /// </summary>
        protected CartoonProjectCreator()
        {
        }

        /// <summary>
        /// Updates the specified first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="document">The document.</param>
        /// <param name="email">The email.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="miniBio">The mini bio.</param>
        /// <param name="isResponsible">if set to <c>true</c> [is responsible].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cartoonProject">The cartoon project.</param>
        public void Update(
            string firstName,
            string lastName,
            string document,
            string email,
            string cellPhone,
            string phoneNumber,
            string miniBio,
            bool isResponsible,
            int userId,
            CartoonProject cartoonProject)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Document = document;
            this.Email = email;
            this.CellPhone = cellPhone;
            this.PhoneNumber = phoneNumber;
            this.MiniBio = miniBio;
            this.IsResponsible = isResponsible;
            this.CartoonProject = cartoonProject;

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
            if (!string.IsNullOrEmpty(this.FirstName) && this.FirstName?.Trim().Length > FirstNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.FirstName, FirstNameMaxLength, FirstNameMinLength), new string[] { "FirstName" }));
            }

            if (!string.IsNullOrEmpty(this.LastName) && this.LastName?.Trim().Length > LastNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.LastNames, LastNameMaxLength, LastNameMinLength), new string[] { "LastName" }));
            }

            if (!string.IsNullOrEmpty(this.Document) && this.Document?.Trim().Length > DocumentMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Document, DocumentMaxLength, DocumentMinLength), new string[] { "Document" }));
            }

            if (!string.IsNullOrEmpty(this.Email) && this.Email?.Trim().Length > EmailMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Email, EmailMaxLength, EmailMinLength), new string[] { "Email" }));
            }

            if (!string.IsNullOrEmpty(this.CellPhone) && this.CellPhone?.Trim().Length > CellPhoneMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.CellPhone, CellPhoneMaxLength, CellPhoneMinLength), new string[] { "CellPhone" }));
            }

            if (!string.IsNullOrEmpty(this.PhoneNumber) && this.PhoneNumber?.Trim().Length > PhoneNumberMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.PhoneNumber, PhoneNumberMaxLength, PhoneNumberMinLength), new string[] { "PhoneNumber" }));
            }

            if (!string.IsNullOrEmpty(this.MiniBio) && this.MiniBio?.Trim().Length > MiniBioMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.MiniBio, MiniBioMaxLength, MiniBioMinLength), new string[] { "MiniBio" }));
            }
        }

        #endregion
    }
}

