// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="User.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int? UserInterfaceLanguageId { get; set; }

        public virtual Collaborator Collaborator { get; set; }
        public virtual Language UserInterfaceLanguage { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserUseTerm> UserUseTerms { get; set; }
        public virtual ICollection<Holding> UpdatedHoldings { get; set; }
        public virtual ICollection<Organization> UpdatedOrganizations { get; set; }
        public virtual ICollection<Collaborator> UpdatedCollaborators { get; set; }

        //public override ValidationResult ValidationResult { get; set; }

        /// <summary>Initializes a new instance of the <see cref="User"/> class.</summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        /// <param name="roles">The roles.</param>
        public User(string fullName, string email, List<Role> roles)
        {
            this.Active = true;
            this.Name = fullName?.Trim();
            this.UserName = this.Email = email?.Trim();
            this.EmailConfirmed = false;
            this.SecurityStamp = Guid.NewGuid().ToString().ToLowerInvariant();
            this.PhoneNumberConfirmed = false;
            this.TwoFactorEnabled = false;
            this.LockoutEnabled = true;
            this.AccessFailedCount = 0;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.SynchronizeRoles(roles);
        }

        /// <summary>Initializes a new instance of the <see cref="User"/> class.</summary>
        protected User()
        {
        }

        /// <summary>Updates the specified full name.</summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="email">The email.</param>
        /// <param name="roles">The roles.</param>
        public void Update(string fullName, string email, List<Role> roles)
        {
            this.Name = fullName?.Trim();
            this.UserName = this.Email = email?.Trim();
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.SynchronizeRoles(roles);
        }

        /// <summary>Deletes this instance.</summary>
        public void Delete(List<Role> roles)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.SynchronizeRoles(roles);
        }

        /// <summary>Called when [access data].</summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="passwordHash">The password hash.</param>
        public void OnboardAccessData(string fullName, string passwordHash)
        {
            this.Name = fullName?.Trim();
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.PasswordHash = passwordHash;
        }

        #region Roles

        /// <summary>Synchronizes the roles.</summary>
        /// <param name="roles">The roles.</param>
        private void SynchronizeRoles(List<Role> roles)
        {
            if (this.Roles == null)
            {
                this.Roles = new List<Role>();
            }

            this.DeleteRoles(roles);

            if (roles?.Any() != true)
            {
                return;
            }

            // Create roles
            foreach (var role in roles)
            {
                var roleDb = this.Roles.FirstOrDefault(r => r.Id == role.Id);
                if (roleDb == null)
                {
                    this.CreateRole(role);
                }
            }
        }

        /// <summary>Deletes the roles.</summary>
        /// <param name="newRoles">The new roles.</param>
        private void DeleteRoles(List<Role> newRoles)
        {
            var rolesToDelete = this.Roles.Where(db => newRoles?.Select(r => r.Id)?.Contains(db.Id) == false).ToList();
            foreach (var roleToDelete in rolesToDelete)
            {
                this.Roles.Remove(roleToDelete);
            }
        }

        /// <summary>Creates the role.</summary>
        /// <param name="role">The role.</param>
        private void CreateRole(Role role)
        {
            this.Roles.Add(role);
        }

        #endregion

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

        public void UpdateInterfaceLanguage(Language language)
        {
            this.UserInterfaceLanguageId = language?.Id;
            this.UserInterfaceLanguage = language;
            this.UpdateDate = DateTime.Now;
        }

        #endregion
    }
}