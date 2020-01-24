// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-24-2020
// ***********************************************************************
// <copyright file="AttendeeLogisticSponsorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>
    /// AttendeeLogisticSponsorMap
    /// </summary>
    public class AttendeeLogisticSponsorMap : EntityTypeConfiguration<AttendeeLogisticSponsor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeLogisticSponsorMap" /> class.
        /// </summary>
        public AttendeeLogisticSponsorMap()
        {
            this.ToTable("AttendeeLogisticSponsors");
        }
    }
}