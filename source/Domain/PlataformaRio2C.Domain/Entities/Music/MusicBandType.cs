// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="MusicBandType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>MusicBandType</summary>
    public class MusicBandType : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBandType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayOrder">The display order.</param>
        public MusicBandType(string name, int displayOrder)
        {
            this.Name = name;
            this.DisplayOrder = displayOrder;
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBandType"/> class.</summary>
        protected MusicBandType()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="name">The name.</param>
        /// <param name="displayOrder">The display order.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            string name,
            int displayOrder,
            int userId)
        {
            this.Name = name?.Trim();
            this.DisplayOrder = displayOrder;

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

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name?.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();
            this.ValidateDisplayOrder();

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

        /// <summary>Validates the display order.</summary>
        public void ValidateDisplayOrder()
        {
            if (this.DisplayOrder <= 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanValue, Labels.DisplayOrder, 0), new string[] { "DisplayOrder" }));
            }
        }

        #endregion
    }
}