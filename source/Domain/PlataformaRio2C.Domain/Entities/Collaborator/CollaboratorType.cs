// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="CollaboratorType.cs" company="Softo">
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
        public static CollaboratorType AdminMusic = new CollaboratorType(new Guid("A838C20B-DD55-4B9D-AECB-37A7E5320DB0"), "Admin | Music");
        public static CollaboratorType AdminInnovation = new CollaboratorType(new Guid("ADF9A699-A8BC-4A1D-8971-C0AC7776D335"), "Admin | Innovation");
        public static CollaboratorType AdminCartoon = new CollaboratorType(new Guid("C55E9C0C-2432-422C-87C6-199457A7C555"), "Admin | Cartoon");
        public static CollaboratorType AdminEditorial = new CollaboratorType(new Guid("3CC40A76-5E69-43E0-872E-2DA26C3C1434"), "Admin | Editorial");
        public static CollaboratorType AdminConferences = new CollaboratorType(new Guid("203D6BFB-3009-4E7E-8BE9-A4F02DA795BB"), "Admin | Conferences");
        public static CollaboratorType AdminCreator = new CollaboratorType(new Guid("AF657A8E-182D-4E4C-92AF-2CC4FCEA9592"), "Admin | Creator");

        /// <summary>
        /// TODO: Create ProducerExecutiveAudiovisual, ProducerExecutiveMusic, ProducerExecutiveInnovation, ProducerExecutiveCartoon
        /// </summary>
        public static CollaboratorType PlayerExecutiveAudiovisual = new CollaboratorType(new Guid("2D6F0E07-7990-458A-8207-1471DC3D1833"), "Player Executive | Audiovisual");
        public static CollaboratorType PlayerExecutiveMusic = new CollaboratorType(new Guid("F05C6213-5CDE-46B8-A617-DF339D9903A9"), "Player Executive | Music");
        public static CollaboratorType PlayerExecutiveInnovation = new CollaboratorType(new Guid("7E4909E0-3DE9-4B55-A678-3C4C277A89DA"), "Player Executive | Innovation");
        public static CollaboratorType PlayerExecutiveCartoon = new CollaboratorType(new Guid("E632C88F-73D2-4EBA-9704-408EF4397DC2"), "Player Executive | Cartoon");

        public static CollaboratorType ComissionAudiovisual = new CollaboratorType(new Guid("60AAFB26-B483-425F-BFA6-ED0D45F3CBCB"), "Commission | Audiovisual");
        public static CollaboratorType ComissionMusic = new CollaboratorType(new Guid("3633CF67-840F-4061-B480-C075A5E9F5EE"), "Commission | Music");
        public static CollaboratorType ComissionInnovation = new CollaboratorType(new Guid("758A53BB-7C3C-4B6F-967B-C6E613568586"), "Commission | Innovation");
        public static CollaboratorType ComissionCartoon = new CollaboratorType(new Guid("A2D0F90D-EA2E-4226-A3EC-47C3360CA1C0"), "Commission | Cartoon");
        public static CollaboratorType ComissionCreator = new CollaboratorType(new Guid("0A043B0C-547A-4F3E-BF79-FB47E520ADB3"), "Commission | Creator");

        public static CollaboratorType Speaker = new CollaboratorType(new Guid("5DA172D8-8D4A-493B-9EEE-F544805A511F"), "Speaker");
        public static CollaboratorType Industry = new CollaboratorType(new Guid("4B0DD2CA-12AE-4357-BEC4-BA4D3820351D"), "Industry");
        public static CollaboratorType Creator = new CollaboratorType(new Guid("1A3BB310-44D0-4677-9938-394C138FD77C"), "Creator");
        public static CollaboratorType Summit = new CollaboratorType(new Guid("536824FB-E98D-4949-B6BE-E6E94D8329E4"), "Summit");
        public static CollaboratorType Festvalia = new CollaboratorType(new Guid("C23C069D-0E3F-4E52-A96D-1F0ABD79E82D"), "Festvalia");
        public static CollaboratorType Music = new CollaboratorType(new Guid("1610EB14-D2E0-4B09-81F9-F904C1FF37B5"), "Music");
        public static CollaboratorType Innovation = new CollaboratorType(new Guid("E1A6AEEE-15FD-4BDB-B899-ACC462F30258"), "Innovation");
        public static CollaboratorType Cartoon = new CollaboratorType(new Guid("A1BBA990-1A08-4381-B233-E0AECE6532DB"), "Cartoon");

        #endregion

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int RoleId { get; private set; }

        public virtual Role Role { get; private set; }

        public virtual ICollection<AttendeeCollaboratorType> AttendeeCollaboratorTypes { get; private set; }
        public virtual ICollection<AttendeeSalesPlatformTicketType> AttendeeSalesPlatformTicketTypes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorType"/> class.
        /// </summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user identifier.</param>
        public CollaboratorType(Guid collaboratorTypeUid, string name, string description, int userId)
        {
            this.Uid = collaboratorTypeUid;
            this.Name = name?.Trim();
            this.Description = description?.TrimStart().TrimEnd();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorType"/> class.
        /// </summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        private CollaboratorType(Guid collaboratorTypeUid, string name, string description = "Description do CollaboratorType")
        {
            this.Uid = collaboratorTypeUid;
            this.Name = name?.Trim();
            //this.Description = description?.TrimStart().TrimEnd();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorType"/> class.
        /// </summary>
        public CollaboratorType()
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