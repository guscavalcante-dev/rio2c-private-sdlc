// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
// ***********************************************************************
// <copyright file="Holding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities.Validations;
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
        public Holding(Guid uid, string name, int userId, bool isImageUploaded)
        {
            //this.Uid = uid;
            this.Descriptions = new List<HoldingDescription>();
            this.SetName(name);
            this.IsImageUploaded = isImageUploaded;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

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

        /// <summary>Adds the description.</summary>
        /// <param name="description">The description.</param>
        public void AddDescription(HoldingDescription description)
        {
            this.Descriptions.Add(description);
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new HoldingIsConsistent().Valid(this));         

            //if (Image != null)
            //{
            //    ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
            //    ValidationResult.Add(new HoldingImageIsConsistent().Valid(this));
            //}

            return ValidationResult.IsValid;
        }
    }
}
