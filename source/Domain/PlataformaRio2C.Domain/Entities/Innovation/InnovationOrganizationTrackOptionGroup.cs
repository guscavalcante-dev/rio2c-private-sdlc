// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-27-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-14-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class InnovationOrganizationTrackOptionGroup.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class InnovationOrganizationTrackOptionGroup : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 100;

        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual List<InnovationOrganizationTrackOption> InnovationOrganizationTrackOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionGroup" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public InnovationOrganizationTrackOptionGroup(string name, int userId)
        {
            this.Name = name;

            this.IsActive = true;
            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionGroup"/> class.
        /// </summary>
        public InnovationOrganizationTrackOptionGroup()
        {

        }

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { nameof(Name) }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { nameof(Name) }));
            }
        }

        #endregion
    }
}
