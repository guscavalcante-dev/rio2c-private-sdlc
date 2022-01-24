// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectTeaserLink.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CartoonProjectTeaserLink</summary>
    public class CartoonProjectTeaserLink : Entity
    {
        public static readonly int ValueMinLength = 1;
        public static readonly int ValueMaxLength = 3000;

        public int CartoonProjectId { get; private set; }
        public string Value { get; private set; }       

        public virtual CartoonProject CartoonProject { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectTeaserLink"/> class.</summary>
        /// <param name="value">The value.</param>
        /// <param name="userId">The user identifier.</param>
        public CartoonProjectTeaserLink(string value, int userId)
        {
            this.Value = value?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectTeaserLink"/> class.</summary>
        protected CartoonProjectTeaserLink()
        {
        }

        /// <summary>Updates the specified value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string value, int userId)
        {
            this.Value = value?.Trim();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateValue();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the value.</summary>
        public void ValidateValue()
        {
            //if (string.IsNullOrEmpty(this.Value?.Trim()))
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Descriptions), new string[] { "Descriptions" }));
            //}

            if (this.Value?.Trim().Length < ValueMinLength || this.Value?.Trim().Length > ValueMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TeaserLinks, ValueMaxLength, ValueMinLength), new string[] { "TeaserLinks" }));
            }
        }

        #endregion
    }
}