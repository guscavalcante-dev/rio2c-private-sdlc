// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
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
        public virtual string UserName { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string PasswordNew { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }        

        public override ValidationResult ValidationResult { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<UserUseTerm> UserUseTerms { get; set; }

        public virtual ICollection<Holding> UpdatedHoldings { get; set; }
        public virtual ICollection<Organization> UpdatedOrganizations { get; set; }

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
