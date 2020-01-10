// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="PresentationFormat.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>PresentationFormat</summary>
    public class PresentationFormat : Entity
    {
        public static readonly int NameMinLength = 1;
        public static readonly int NameMaxLength = 500;

        public int EditionId { get; private set; }
        public string Name { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual ICollection<ConferencePresentationFormat> ConferencePresentationFormats { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="PresentationFormat"/> class.</summary>
        /// <param name="presentationFormatUid">The presentation format uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="presentationFormatNames">The presentation format names.</param>
        /// <param name="userId">The user identifier.</param>
        public PresentationFormat(
            Guid presentationFormatUid,
            Edition edition,
            List<PresentationFormatName> presentationFormatNames,
            int userId)
        {
            //this.Uid = presentationFormatUid;
            this.EditionId = edition?.Id ?? 0;
            this.Edition = edition;
            this.UpdateName(presentationFormatNames);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="PresentationFormat"/> class.</summary>
        protected PresentationFormat()
        {
        }

        /// <summary>Updates the main information.</summary>
        /// <param name="presentationFormatNames">The presentation format names.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMainInformation(
            List<PresentationFormatName> presentationFormatNames,
            int userId)
        {
            this.UpdateName(presentationFormatNames);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.DeleteConferencesPresentationFormats(userId);

            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Presentation Format Names

        /// <summary>Updates the name.</summary>
        /// <param name="presentationFormatNames">The presentation format names.</param>
        private void UpdateName(List<PresentationFormatName> presentationFormatNames)
        {
            var name = string.Empty;
            foreach (var languageCode in Language.CodesOrder)
            {
                name += (!string.IsNullOrEmpty(name) ? " " + Language.Separator + " " : String.Empty) +
                        presentationFormatNames?.FirstOrDefault(vtc => vtc.Language.Code == languageCode)?.Value;
            }

            this.Name = name;
        }

        #endregion

        #region Conference Presentation Formats

        /// <summary>Deletes the conferences presentation formats.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferencesPresentationFormats(int userId)
        {
            if (this.ConferencePresentationFormats?.Any() != true)
            {
                return;
            }

            foreach (var conferencePresentationFormat in this.ConferencePresentationFormats.Where(c => !c.IsDeleted))
            {
                conferencePresentationFormat.Delete(userId);
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateEdition();
            this.ValidateName();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the edition.</summary>
        public void ValidateEdition()
        {
            if (this.Edition == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Edition), new string[] { "Edition" }));
            }
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
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Descriptions, NameMaxLength, NameMinLength), new string[] { "Name" }));
            }
        }

        #endregion
    }
}