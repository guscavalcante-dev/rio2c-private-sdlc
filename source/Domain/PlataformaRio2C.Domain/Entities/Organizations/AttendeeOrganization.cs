// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="AttendeeOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeOrganization</summary>
    public class AttendeeOrganization : Entity
    {
        public int EditionId { get; private set; }
        public int OrganizationId { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Organization Organization { get; private set; }
        public virtual ICollection<AttendeeOrganizationType> AttendeeOrganizationTypes { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organization">The organization.</param>
        public AttendeeOrganization(Edition edition, Organization organization)
        {
            this.Edition = edition;
            this.Organization = organization;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganization"/> class.</summary>
        protected AttendeeOrganization()
        {
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
            //ValidationResult = new ValidationResult();

            //ValidationResult.Add(new PlayerIsConsistent().Valid(this));

            //if (Image != null)
            //{
            //    ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
            //    ValidationResult.Add(new PlayerImageIsConsistent().Valid(this));
            //}

            //return ValidationResult.IsValid;
        }
    }
}