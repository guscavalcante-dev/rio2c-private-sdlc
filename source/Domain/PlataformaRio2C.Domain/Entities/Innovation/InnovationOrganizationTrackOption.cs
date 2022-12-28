// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2022
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOption.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
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
        public InnovationOrganizationTrackOption()
        {

        }

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

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
