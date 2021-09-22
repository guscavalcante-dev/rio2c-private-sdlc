// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="OrganizationType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>OrganizationType</summary>
    public class OrganizationType : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;

        #region Configurations

        public static OrganizationType Player = new OrganizationType(new Guid("936B3262-8B8F-472C-94AD-3A2B925DD0AE"), "Player");
        public static OrganizationType Producer = new OrganizationType(new Guid("7CE5A34F-E31F-4C26-BED9-CDD6A0206185"), "Producer");
        public static OrganizationType Startup = new OrganizationType(new Guid("F2EFDBAA-27BD-42BD-BF29-A8DAED6093FF"), "Startup");
        public static OrganizationType Investor = new OrganizationType(new Guid("7EB327A9-95E8-4514-8E66-39510FC9ED03"), "Investor");
        public static OrganizationType MusicBand = new OrganizationType(new Guid("D077BA5C-2982-4B69-95D4-D9AA1BF8E7F4"), "Music Band");
        public static OrganizationType Recorder = new OrganizationType(new Guid("243AAFB2-B610-49B4-B9BC-33CDF631C367"), "Recorder");

        #endregion

        public string Name { get; private set; }
        public int RelatedProjectTypeId { get; private set; }
        public bool IsSeller { get; private set; }

        public virtual ProjectType RelatedProjectType { get; private set; }

        public virtual ICollection<AttendeeOrganizationType> AttendeeOrganizationTypes { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationType"/> class.</summary>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public OrganizationType(Guid organizationTypeUid, string name, int userId)
        {
            this.Uid = organizationTypeUid;
            this.Name = name;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationType"/> class.</summary>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="name">The name.</param>
        private OrganizationType(Guid organizationTypeUid, string name)
        {
            this.Uid = organizationTypeUid;
            this.Name = name;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationType" /> class.
        /// </summary>
        protected OrganizationType()
        {
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}
