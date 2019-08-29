// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-29-2019
// ***********************************************************************
// <copyright file="User.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>User</summary>
    public class User : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 150;
        public static readonly int UserNameMinLength = 1;
        public static readonly int UserNameMaxLength = 256;
        public static readonly int EmailMaxLength = 256;

        public string Name { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordNew { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }        

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserUseTerm> UserUseTerms { get; set; }

        public virtual Collaborator Collaborator { get; set; }
        public virtual ICollection<Holding> UpdatedHoldings { get; set; }
        public virtual ICollection<Organization> UpdatedOrganizations { get; set; }
        public virtual ICollection<Collaborator> UpdatedCollaborators { get; set; }

        //public override ValidationResult ValidationResult { get; set; }

        /// <summary>Initializes a new instance of the <see cref="User"/> class for pre-register.</summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        public User(string fullName, string email)
        {
            this.Active = true;
            this.Name = fullName?.Trim();
            this.UserName = this.Email = email?.Trim();
            this.EmailConfirmed = false;
            this.SecurityStamp = Guid.NewGuid().ToString().ToLowerInvariant();
            this.PhoneNumberConfirmed = false;
            this.TwoFactorEnabled = false;
            this.LockoutEnabled = false;
            this.AccessFailedCount = 0;
            this.CreateDate = this.UpdateDate = DateTime.Now;
        }

        /// <summary>Initializes a new instance of the <see cref="User"/> class.</summary>
        protected User()
        {
        }

        /// <summary>Updates the specified full name.</summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        public void Update(string fullName, string email)
        {
            this.Name = fullName?.Trim();
            this.UserName = this.Email = email?.Trim();
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
        }

        /// <summary>Deletes this instance.</summary>
        public void Delete()
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
        }

        #region Old

        public User(string email)
        {
            SetActive(true);
            SetEmail(email);            
        }      


        public void SetEmail(string value)
        {
            Email = value;
            SetUserName(value);
        }

        public void SetName(string value)
        {
            Name = value;
        }

        public void SetUserName(string value)
        {
            UserName = value;
        }

        public void SetActive(bool value)
        {
            Active = value;
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateUserName();
            this.ValidateEmail();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "FirstName" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "FirstName" }));
            }
        }


        /// <summary>Validates the name of the user.</summary>
        public void ValidateUserName()
        {
            if (string.IsNullOrEmpty(this.UserName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Email), new string[] { "Email" }));
            }

            if (this.UserName?.Trim().Length < UserNameMinLength || this.UserName?.Trim().Length > UserNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Email, UserNameMaxLength, UserNameMinLength), new string[] { "Email" }));
            }
        }

        /// <summary>Validates the email.</summary>
        public void ValidateEmail()
        {
            if (!string.IsNullOrEmpty(this.Email) && this.Email?.Trim().Length > EmailMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Email, EmailMaxLength, 1), new string[] { "Email" }));
            }
        }

        #endregion
    }
}