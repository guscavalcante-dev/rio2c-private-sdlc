// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="AttendeeSalesPlatform.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeSalesPlatform</summary>
    public class AttendeeSalesPlatform : Entity
    {
        public int EditionId { get; private set; }
        public int SalesPlatformId { get; private set; }
        public string SalesPlatformEventid { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime? LastSalesPlatformOrderDate { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual SalesPlatform SalesPlatform { get; private set; }

        public virtual ICollection<AttendeeSalesPlatformTicketType> AttendeeSalesPlatformTicketTypes { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatform"/> class.</summary>
        protected AttendeeSalesPlatform()
        {
        }

        /// <summary>
        /// Updates the last sales platform order date.
        /// </summary>
        /// <param name="lastSalesPlatformOrderDate">The last sales platform order date.</param>
        public void UpdateLastSalesPlatformOrderDate(DateTime? lastSalesPlatformOrderDate)
        {
            // Only updates if lastOrderUpdatedDate is greater than current to avoid import already imported attendees
            if (!this.LastSalesPlatformOrderDate.HasValue || lastSalesPlatformOrderDate > this.LastSalesPlatformOrderDate)
            {
                this.LastSalesPlatformOrderDate = lastSalesPlatformOrderDate;
                this.UpdateDate = DateTime.Now;
            }
        }

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }

        #endregion
    }
}