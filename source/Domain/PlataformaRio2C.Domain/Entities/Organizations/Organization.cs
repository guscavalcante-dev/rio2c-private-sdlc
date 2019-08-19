// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="Organization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Organization</summary>
    public class Organization : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;
        public static readonly int CompanyNameMaxLength = 100;
        public static readonly int WebSiteMaxLength = 100;
        public static readonly int SocialMediaMaxLength = 256;
        public static readonly int TradeNameMaxLength = 100;

        public int? HoldingId { get; private set; }
        public string Name { get; private set; }
        public string CompanyName { get; private set; }
        public string TradeName { get; private set; }
        public string Document { get; private set; }
        public string Website { get; private set; }
        public string SocialMedia { get; private set; }
        public string PhoneNumber { get; private set; }
        public int? AddressId { get; private set; }
        public DateTime? ImageUploadDate { get; private set; }
        
        public virtual Holding Holding { get; private set; }
        public virtual User Updater { get; private set; }

        public virtual ICollection<OrganizationDescription> Descriptions { get; private set; }
        public virtual ICollection<AttendeeOrganization> AttendeeOrganizations { get; private set; }

        //public virtual Address Address { get; private set; }
        //public virtual ICollection<PlayerDescription> Descriptions { get; private set; }
        //public virtual ICollection<PlayerInterest> Interests { get; private set; }
        //public virtual ICollection<Collaborator> Collaborators { get; private set; }
        //public virtual ICollection<Collaborator> CollaboratorsOld { get; private set; }
        //public virtual ICollection<PlayerActivity> PlayerActivitys { get; private set; }
        //public virtual ICollection<PlayerTargetAudience> PlayerTargetAudience { get; private set; }
        //public virtual ICollection<PlayerRestrictionsSpecifics> RestrictionsSpecifics { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="holding">The holding.</param>
        public Organization(string name, Holding holding)
        {
            this.SetName(name);
            this.SetHolding(holding);
        }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class.</summary>
        protected Organization()
        {
        }

        /// <summary>Sets the name.</summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            this.Name = name;
        }

        /// <summary>Sets the holding.</summary>
        /// <param name="holding">The holding.</param>
        public void SetHolding(Holding holding)
        {
            this.Holding = holding;
            this.HoldingId = holding.Id;
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
            //ValidationResult = new ValidationResult();

            //ValidationResult.Add(new PlayerIsConsistent().Valid(this));

            //if (Image != null)
            //{
            //    ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
            //    ValidationResult.Add(new PlayerImageIsConsistent().Valid(this));
            //}

            //return ValidationResult.IsValid;
        }
    }
}
