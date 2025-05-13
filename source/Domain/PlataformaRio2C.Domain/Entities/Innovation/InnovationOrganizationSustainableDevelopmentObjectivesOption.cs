// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 12-01-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 12-01-2022
// ***********************************************************************
// <copyright file="InnovationOrganizationSustainableDevelopmentObjectivesOption.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class InnovationOrganizationSustainableDevelopmentObjectivesOption.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class InnovationOrganizationSustainableDevelopmentObjectivesOption : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool HasAdditionalInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationSustainableDevelopmentObjectivesOption"/> class.
        /// </summary>
        public InnovationOrganizationSustainableDevelopmentObjectivesOption()
        {

        }

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        /// <summary>Gets the description translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetDesctiptionTranslation(string languageCode)
        {
            return this.Description.GetSeparatorTranslation(languageCode, Language.Separator);
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
