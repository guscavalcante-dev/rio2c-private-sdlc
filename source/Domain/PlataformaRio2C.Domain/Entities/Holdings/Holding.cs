﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="Holding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Holding</summary>
    public class Holding : AggregateRoot
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 81;

        public string Name { get; private set; }
        public DateTimeOffset? ImageUploadDate { get; private set; }

        public virtual User Updater { get; private set; }

        public virtual ICollection<HoldingDescription> Descriptions { get; private set; }
        public virtual ICollection<Organization> Organizations { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="Holding"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public Holding(Guid uid, string name, bool isImageUploaded, List<HoldingDescription> descriptions, int userId)
        {
            //this.Uid = uid;
            this.Name = name?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.SynchronizeDescriptions(descriptions, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="Holding"/> class.</summary>
        protected Holding()
        {
        }

        /// <summary>Updates the specified name.</summary>
        /// <param name="name">The name.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string name, bool isImageUploaded, bool isImageDeleted, List<HoldingDescription> descriptions, int userId)
        {
            //this.Uid = uid;
            this.Name = name?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.SynchronizeDescriptions(descriptions, userId);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.DeleteOrganizations(userId);
            this.DeleteDescriptions(null, userId);
            this.UpdateImageUploadDate(false, true);

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the image upload date.</summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        private void UpdateImageUploadDate(bool isImageUploaded, bool isImageDeleted)
        {
            if (isImageUploaded)
            {
                this.ImageUploadDate = DateTime.UtcNow;
            }
            else if (isImageDeleted)
            {
                this.ImageUploadDate = null;
            }
        }

        #region Organizations

        /// <summary>Deletes the organizations.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteOrganizations(int userId)
        {
            foreach (var organization in this.FindAllOrganizationsNotDeleted())
            {
                organization.Delete(null, null, userId);
            }
        }

        /// <summary>Finds all organizations not deleted.</summary>
        /// <returns></returns>
        private List<Organization> FindAllOrganizationsNotDeleted()
        {
            return this.Organizations?.Where(o => !o.IsDeleted)?.ToList();
        }

        #endregion

        #region Descriptions

        /// <summary>Synchronizes the descriptions.</summary>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeDescriptions(List<HoldingDescription> descriptions, int userId)
        {
            if (this.Descriptions == null)
            {
                this.Descriptions = new List<HoldingDescription>();
            }

            this.DeleteDescriptions(descriptions, userId);

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
        /// <param name="newDescriptions">The new descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteDescriptions(List<HoldingDescription> newDescriptions, int userId)
        {
            var descriptionsToDelete = this.Descriptions.Where(db => newDescriptions?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false).ToList();
            foreach (var descriptionToDelete in descriptionsToDelete)
            {
                descriptionToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the description.</summary>
        /// <param name="description">The description.</param>
        private void CreateDescription(HoldingDescription description)
        {
            this.Descriptions.Add(description);
        }

        #endregion

        #region Validation

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateDescriptions();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        /// <summary>Validates the descriptions.</summary>
        public void ValidateDescriptions()
        {
            foreach (var description in this.Descriptions?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(description.ValidationResult);
            }
        }

        #endregion
    }
}