// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-12-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCompetitor.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganizationCompetitor.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganizationCompetitor : Entity
    {
        public static readonly int NameMaxLenght = 300;

        public int AttendeeInnovationOrganizationId { get; set; }
        public string Name { get; set; }

        public virtual AttendeeInnovationOrganization AttendeeInnovationOrganization { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCompetitor" /> class.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationCompetitor(
            AttendeeInnovationOrganization attendeeInnovationOrganization, 
            string name,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.Name = name;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCompetitor"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationCompetitor()
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

            this.ValidateName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        private void ValidateName()
        {
            if (this.Name.Length > NameMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Name), NameMaxLenght, 1), new string[] { nameof(Name) }));
            }
        }

        #endregion
    }
}
