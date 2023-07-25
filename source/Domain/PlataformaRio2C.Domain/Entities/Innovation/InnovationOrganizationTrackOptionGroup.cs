// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-27-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-25-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
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

        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }

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

        #region Update

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(string name, int userId)
        {
            this.Name = name;
            this.SetUpdateDate(userId);
        }

        #endregion

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public new void Delete(int userId)
        {
            this.DeleteInnovationOrganizationTrackOptions(userId);

            base.Delete(userId);
        }

        /// <summary>
        /// Deletes the innovation organization track options.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteInnovationOrganizationTrackOptions(int userId)
        {
            foreach (var innovationOrganizationTrackOption in this.InnovationOrganizationTrackOptions)
            {
                innovationOrganizationTrackOption?.Delete(userId);
            }
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
