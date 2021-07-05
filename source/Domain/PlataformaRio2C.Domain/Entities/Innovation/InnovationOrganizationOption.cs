// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationOption.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class InnovationOrganization.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class InnovationOrganizationOption : Entity
    {
        public int InnovationOrganizationId { get; private set; }
        public int InnovationOptionId { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual InnovationOrganization InnovationOrganization { get; private set; }
        public virtual InnovationOption InnovationOption { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationOption"/> class.
        /// </summary>
        /// <param name="innovationOrganization">The innovation organization.</param>
        /// <param name="innovationOption">The innovation option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public InnovationOrganizationOption(
            InnovationOrganization innovationOrganization,
            InnovationOption innovationOption, 
            string additionalInfo,
            int userId)
        {
            this.InnovationOrganization = innovationOrganization;
            this.InnovationOption = innovationOption;
            //this.InnovationOrganizationId = innovationOrganization.Id;
            //this.InnovationOptionId = innovationOption.Id;
            this.AdditionalInfo = additionalInfo;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationOption"/> class.
        /// </summary>
        public InnovationOrganizationOption()
        {

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
