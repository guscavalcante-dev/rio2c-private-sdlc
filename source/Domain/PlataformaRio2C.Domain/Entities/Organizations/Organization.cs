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
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Organization</summary>
    public class Organization : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;
        public static readonly int CompanyNameMaxLength = 100;
        public static readonly int TradeNameMaxLength = 100;
        public static readonly int WebSiteMaxLength = 100;
        public static readonly int SocialMediaMaxLength = 256;

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
        /// <param name="uid">The uid.</param>
        /// <param name="holding">The holding.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="name">The name.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="website">The website.</param>
        /// <param name="socialMedia">The social media.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public Organization(
            Guid uid, 
            Holding holding, 
            Edition edition,
            OrganizationType organizationType,
            string name, 
            string companyName, 
            string tradeName, 
            string document, 
            string website, 
            string socialMedia, 
            string phoneNumber, 
            int? addressId, 
            bool isImageUploaded, 
            List<OrganizationDescription> descriptions, 
            int userId)
        {
            //this.Uid = uid;
            this.Holding = holding;
            this.HoldingId = holding?.Id;
            this.Name = name?.Trim();
            this.CompanyName = companyName?.Trim();
            this.TradeName = tradeName?.Trim();
            this.Document = document?.Trim();
            this.Website = website?.Trim();
            this.SocialMedia = socialMedia?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.AddressId = addressId;
            this.ImageUploadDate = isImageUploaded ? (DateTime?)DateTime.Now : null;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeDescriptions(descriptions);
            this.SynchronizeAttendeeOrganizations(edition, organizationType, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="Organization"/> class.</summary>
        protected Organization()
        {
        }

        #region Descriptions

        /// <summary>Synchronizes the descriptions.</summary>
        /// <param name="descriptions">The descriptions.</param>
        private void SynchronizeDescriptions(List<OrganizationDescription> descriptions)
        {
            if (this.Descriptions == null)
            {
                this.Descriptions = new List<OrganizationDescription>();
            }

            if (descriptions?.Any() != true)
            {
                return;
            }

            // Create or update descriptions
            foreach (var description in descriptions)
            {
                var descriptionDb = this.Descriptions.FirstOrDefault(d => d.Language.Code == description.Language.Code);
                if (descriptionDb != null)
                {
                    descriptionDb.Update(description);
                }
                else
                {
                    this.CreateDescription(description);
                }
            }
        }

        /// <summary>Deletes the descriptions.</summary>
        /// <param name="descriptions">The descriptions.</param>
        /// <returns></returns>
        public List<OrganizationDescription> DeleteDescriptions(List<OrganizationDescription> descriptions)
        {
            var descriptionsToDelete = this.Descriptions.Where(db => descriptions?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false).ToList();

            foreach (var descriptionToDelete in descriptionsToDelete)
            {
                this.Descriptions.Remove(descriptionToDelete);
            }

            return descriptionsToDelete;
        }

        /// <summary>Creates the description.</summary>
        /// <param name="description">The description.</param>
        private void CreateDescription(OrganizationDescription description)
        {
            this.Descriptions.Add(description);
        }

        #endregion

        #region Attendee Organizations

        /// <summary>Synchronizes the attendee organizations.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeOrganizations(Edition edition, OrganizationType organizationType, int userId)
        {
            if (this.AttendeeOrganizations == null)
            {
                this.AttendeeOrganizations = new List<AttendeeOrganization>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeeOrganization = this.AttendeeOrganizations.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeOrganization != null)
            {
                return;
            }

            this.AttendeeOrganizations.Add(new AttendeeOrganization(edition, this, organizationType, userId));
        }

        #endregion

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
