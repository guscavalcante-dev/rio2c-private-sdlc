// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="User.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>User</summary>
    public class User : Entity
    {
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

        public override bool IsValid()
        {
            return true;
        }
    }
}