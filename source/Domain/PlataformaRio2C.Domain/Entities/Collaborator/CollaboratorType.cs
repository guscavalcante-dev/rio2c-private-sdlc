// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-09-2020
// ***********************************************************************
// <copyright file="TicketType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CollaboratorType</summary>
    public class CollaboratorType : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 256;

        #region Configurations

        public static CollaboratorType AdminAudiovisual = new CollaboratorType(new Guid("3871F510-C081-4B69-9ECC-8889E791B0CC"), "Admin | Audiovisual");
        public static CollaboratorType AdminLogistic = new CollaboratorType(new Guid("2141F9F7-4037-423C-81BF-7ED27520489A"), "Admin | Logistic");
        public static CollaboratorType CuratorshipAudiovisual = new CollaboratorType(new Guid("4AC5A971-BA73-493B-9749-0F51BB6925B5"), "Curatorship | Audiovisual");
        public static CollaboratorType CuratorshipMusic = new CollaboratorType(new Guid("495B5126-0A9E-4658-AC42-8BBA55818B6F"), "Curatorship | Music");
        public static CollaboratorType CuratorshipInnovation = new CollaboratorType(new Guid("CB1A63D2-5862-4B40-8B8A-7F512E5D046D"), "Curatorship | Innovation");
        public static CollaboratorType ExecutiveAudiovisual = new CollaboratorType(new Guid("2D6F0E07-7990-458A-8207-1471DC3D1833"), "Executive | Audiovisual");
        public static CollaboratorType ExecutiveMusic = new CollaboratorType(new Guid("F05C6213-5CDE-46B8-A617-DF339D9903A9"), "Executive | Music");
        public static CollaboratorType ExecutiveInnovation = new CollaboratorType(new Guid("7E4909E0-3DE9-4B55-A678-3C4C277A89DA"), "Executive | Innovation");
        public static CollaboratorType ComissionAudiovisual = new CollaboratorType(new Guid("60AAFB26-B483-425F-BFA6-ED0D45F3CBCB"), "Commission | Audiovisual");
        public static CollaboratorType ComissionMusic = new CollaboratorType(new Guid("3633CF67-840F-4061-B480-C075A5E9F5EE"), "Commission | Music");
        public static CollaboratorType ComissionInnovation = new CollaboratorType(new Guid("758A53BB-7C3C-4B6F-967B-C6E613568586"), "Commission | Innovation");
        public static CollaboratorType Speaker = new CollaboratorType(new Guid("5DA172D8-8D4A-493B-9EEE-F544805A511F"), "Speaker");
        public static CollaboratorType Industry = new CollaboratorType(new Guid("4B0DD2CA-12AE-4357-BEC4-BA4D3820351D"), "Industry");
        public static CollaboratorType Creator = new CollaboratorType(new Guid("1A3BB310-44D0-4677-9938-394C138FD77C"), "Creator");
        public static CollaboratorType Summit = new CollaboratorType(new Guid("536824FB-E98D-4949-B6BE-E6E94D8329E4"), "Summit");
        public static CollaboratorType Festvalia = new CollaboratorType(new Guid("C23C069D-0E3F-4E52-A96D-1F0ABD79E82D"), "Festvalia");

        #endregion

        public string Name { get; private set; }
        public int RoleId { get; private set; }

        public virtual Role Role { get; private set; }

        public virtual ICollection<AttendeeCollaboratorType> AttendeeCollaboratorTypes { get; private set; }
        public virtual ICollection<AttendeeSalesPlatformTicketType> AttendeeSalesPlatformTicketTypes { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public CollaboratorType(Guid collaboratorTypeUid, string name, int userId)
        {
            this.Uid = collaboratorTypeUid;
            this.Name = name?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="name">The name.</param>
        private CollaboratorType(Guid collaboratorTypeUid, string name)
        {
            this.Uid = collaboratorTypeUid;
            this.Name = name?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorType"/> class.</summary>
        private CollaboratorType()
        {
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the name.</summary>
        public void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "Name" }));
            }

            if (this.Name?.Trim().Length < NameMinLength || this.Name?.Trim().Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        #endregion
    }
}