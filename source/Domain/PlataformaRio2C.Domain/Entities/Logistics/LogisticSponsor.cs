// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="LogisticsSponsor.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>LogisticSponsor</summary>
    public class LogisticSponsor : Entity
    {
        public string Name { get; private set; }
        public bool IsAirfareTicketRequired { get; private set; }
        public bool IsOtherRequired { get; protected set; }
        
        public LogisticSponsor(){}

        public LogisticSponsor(
            List<TranslatedName> names,
            int userId)
        {
            UpdateName(names);

            this.IsDeleted = false;
            this.IsAirfareTicketRequired = false;
            this.IsOtherRequired = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        public virtual ICollection<AttendeeLogisticSponsor> AttendeeLogisticSponsors { get; private set; }
        
        private void UpdateName(List<TranslatedName> names)
        {
            var name = string.Empty;
            foreach (var languageCode in Language.CodesOrder)
            {
                name += (!string.IsNullOrEmpty(name) ? " " + Language.Separator + " " : String.Empty) +
                        names?.FirstOrDefault(vtc => vtc.Language.Code == languageCode)?.Value;
            }

            this.Name = name;
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
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