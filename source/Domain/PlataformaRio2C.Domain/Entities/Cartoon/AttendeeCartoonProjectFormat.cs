// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-12-2021
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectFormat.cs" company="Softo">
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
    /// Class AttendeeCartoonProjectFormat.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeCartoonProjectFormat : Entity
    {
        public int CartoonProjectId { get; set; }
        public int CartoonProjectFormatId { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual CartoonProject CartoonProject { get; private set; }
        public virtual CartoonProjectFormat CartoonProjectFormat { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCartoonProjectFormat"/> class.
        /// </summary>
        /// <param name="cartoonProject">The cartoon project.</param>
        /// <param name="cartoonProjectFormat">The cartoon project format.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCartoonProjectFormat(
            CartoonProject cartoonProject,
            CartoonProjectFormat cartoonProjectFormat, 
            string additionalInfo,
            int userId)
        {
            this.CartoonProject = cartoonProject;
            this.CartoonProjectFormat = cartoonProjectFormat;
            this.AdditionalInfo = additionalInfo;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCartoonProjectFormat"/> class.
        /// </summary>
        public AttendeeCartoonProjectFormat()
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
