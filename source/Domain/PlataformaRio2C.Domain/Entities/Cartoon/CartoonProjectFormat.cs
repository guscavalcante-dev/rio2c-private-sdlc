// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonProjectFormat.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class CartoonProjectFormat.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class CartoonProjectFormat : Entity
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool HasAdditionalInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartoonProjectFormat"/> class.
        /// </summary>
        public CartoonProjectFormat()
        {

        }

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name?.GetSeparatorTranslation(languageCode, Language.Separator);
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
