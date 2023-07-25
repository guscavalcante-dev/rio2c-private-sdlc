// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-25-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOption.cs" company="Softo">
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
    /// Class InnovationOrganizationTrackOption.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class InnovationOrganizationTrackOption : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool HasAdditionalInfo { get; private set; }
        public bool IsActive { get; private set; }
        public int? InnovationOrganizationTrackOptionGroupId { get; private set; }

        public virtual InnovationOrganizationTrackOptionGroup InnovationOrganizationTrackOptionGroup { get; set; }
        public virtual List<AttendeeCollaboratorInnovationOrganizationTrack> AttendeeCollaboratorInnovationOrganizationTracks { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOption"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="innovationOrganizationTrackOptionGroup">The innovation organization track option group.</param>
        /// <param name="userId">The user identifier.</param>
        public InnovationOrganizationTrackOption(
            string name, 
            InnovationOrganizationTrackOptionGroup innovationOrganizationTrackOptionGroup, 
            int userId)
        {
            this.Name = name;
            this.Description = "";
            this.InnovationOrganizationTrackOptionGroupId = innovationOrganizationTrackOptionGroup?.Id;
            this.InnovationOrganizationTrackOptionGroup = innovationOrganizationTrackOptionGroup;

            this.IsActive = true;
            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationTrackOption"/> class.
        /// </summary>
        public InnovationOrganizationTrackOption()
        {

        }

        #region Update

        /// <summary>
        /// Updates the main information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="innovationOrganizationTrackOptionGroup">The innovation organization track option group.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            string name, 
            InnovationOrganizationTrackOptionGroup innovationOrganizationTrackOptionGroup,
            int userId)
        {
            this.Name = name;
            this.InnovationOrganizationTrackOptionGroup = innovationOrganizationTrackOptionGroup;
            this.InnovationOrganizationTrackOptionGroupId = innovationOrganizationTrackOptionGroup.Id;

            this.SetUpdateDate(userId);
        }

        #endregion

        /// <summary>
        /// Gets the name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>
        /// Gets the description translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetDesctiptionTranslation(string languageCode)
        {
            return this.Description.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>
        /// Gets the innovation organization track option group name translation.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetInnovationOrganizationTrackOptionGroupNameTranslation(string languageCode)
        {
            return this.InnovationOrganizationTrackOptionGroup?.Name?.GetSeparatorTranslation(languageCode, Language.Separator);
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
            this.ValidateInnovationOrganizationTrackOptionGroup();

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

        /// <summary>
        /// Validates the innovation organization track option group.
        /// </summary>
        public void ValidateInnovationOrganizationTrackOptionGroup()
        {
            if (this.InnovationOrganizationTrackOptionGroup == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Vertical), new string[] { nameof(InnovationOrganizationTrackOptionGroupId) }));
            }
        }

        #endregion
    }
}
