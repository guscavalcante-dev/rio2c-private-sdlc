// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
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
        protected Holding()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Holding"/> class.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="descriptions"></param>
        public Holding(Guid uid, string name, int userId, bool isImageUploaded, List<HoldingDescription> descriptions)
        {
            //this.Uid = uid;
            this.SetName(name);
            this.IsImageUploaded = isImageUploaded;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDescriptions(descriptions);
        }

        #region Descriptions

        private void CreateDescriptions(List<HoldingDescription> descriptions)
        {
            if (this.Descriptions == null)
            {
                this.Descriptions = new List<HoldingDescription>();
            }

            if (descriptions?.Any() != true)
            {
                return;
            }

            foreach (var description in descriptions)
            {
                this.CreateDescription(description);
            }
        }

        /// <summary>Creates the description.</summary>
        /// <param name="description">The description.</param>
        public void CreateDescription(HoldingDescription description)
        {
            this.Descriptions.Add(description);
        }

        #endregion

        public Holding(string name)
        {
            this.Descriptions = new List<HoldingDescription>();
            this.SetName(name);
        }

        /// <summary>Sets the name.</summary>
        /// <param name="name">The name.</param>
        public void SetName(string name)
        {
            this.Name = name;
        }

        /// <summary>Sets the descriptions.</summary>
        /// <param name="descriptions">The descriptions.</param>
        public void SetDescriptions(IEnumerable<HoldingDescription> descriptions)
        {
            this.Descriptions = descriptions.ToList();
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            //ValidationResult.Add(new HoldingIsConsistent().Valid(this));         

            //if (Image != null)
            //{
            //    ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
            //    ValidationResult.Add(new HoldingImageIsConsistent().Valid(this));
            //}

            return this.ValidationResult.IsValid;
        }

        #region Validation

        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                // TODO: use resources on validation errrors
                //this.ValidationResult.Add(new ValidationError(string.Format("Já existe um holding com o nome '{0}'.", cmd.Name), new string[] { "Name" }););
                this.ValidationResult.Add(new ValidationError("O nome é obrigatório.", new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length > 100)
            {
                this.ValidationResult.Add(new ValidationError("O nome deve ter no máximo 100 caracters.", new string[] { "Name" }));
            }
        }

        #endregion
    }
}