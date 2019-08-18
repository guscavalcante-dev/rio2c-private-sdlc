// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
// ***********************************************************************
// <copyright file="Holding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Holding</summary>
    public class Holding : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }
        public bool IsImageUploaded { get; private set; }

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
            this.IsImageUploaded = isImageUploaded;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeDescriptions(descriptions);
        }

        /// <summary>Initializes a new instance of the <see cref="Holding"/> class.</summary>
        protected Holding()
        {
        }

        /// <summary>Updates the specified name.</summary>
        /// <param name="name">The name.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string name, bool isImageUploaded, List<HoldingDescription> descriptions, int userId)
        {
            //this.Uid = uid;
            this.Name = name?.Trim();
            this.IsImageUploaded = isImageUploaded;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeDescriptions(descriptions);
        }

        #region Descriptions

        /// <summary>Synchronizes the descriptions.</summary>
        /// <param name="descriptions">The descriptions.</param>
        private void SynchronizeDescriptions(List<HoldingDescription> descriptions)
        {
            if (this.Descriptions == null)
            {
                this.Descriptions = new List<HoldingDescription>();
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
        public List<HoldingDescription> DeleteDescriptions(List<HoldingDescription> descriptions)
        {
            var descriptionsToDelete = this.Descriptions.Where(db => descriptions?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false).ToList();

            // Remove transactions from the list
            foreach (var descriptionToDelete in descriptionsToDelete)
            {
                this.Descriptions.Remove(descriptionToDelete);
            }

            return descriptionsToDelete;
        }

        /// <summary>Creates the description.</summary>
        /// <param name="description">The description.</param>
        private void CreateDescription(HoldingDescription description)
        {
            this.Descriptions.Add(description);
        }

        #endregion

        //public Holding(string name)
        //{
        //    this.Descriptions = new List<HoldingDescription>();
        //    this.SetName(name);
        //}

        ///// <summary>Sets the name.</summary>
        ///// <param name="name">The name.</param>
        //public void SetName(string name)
        //{
        //    this.Name = name;
        //}

        ///// <summary>Sets the descriptions.</summary>
        ///// <param name="descriptions">The descriptions.</param>
        //public void SetDescriptions(IEnumerable<HoldingDescription> descriptions)
        //{
        //    this.Descriptions = descriptions.ToList();
        //}

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
            // TODO: use resources on validation errrors
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError("O nome é obrigatório.", new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError($"O nome deve ter entre '{NameMinLength}' e '{NameMaxLength}' caracteres.", new string[] { "Name" }));
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