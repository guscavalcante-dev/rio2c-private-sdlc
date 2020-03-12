// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
// ***********************************************************************
// <copyright file="LogisticMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>LogisticMap</summary>
    public class LogisticMap : EntityTypeConfiguration<Logistic>
    {
        /// <summary>Initializes a new instance of the <see cref="LogisticMap"/> class.</summary>
        public LogisticMap()
        {
            this.ToTable("Logistics");

            this.Property(p => p.AdditionalInfo)
                .HasMaxLength(Logistic.AdditionalInfoMaxLength);

            // Relationships
            this.HasRequired(e => e.AttendeeCollaborator)
                .WithMany(t => t.Logistics)
                .HasForeignKey(e => e.AttendeeCollaboratorId);

            this.HasOptional(t => t.AccommodationAttendeeLogisticSponsor)
                .WithMany()
                //.WithMany(e => e.AccommodationSponsors)
              .HasForeignKey(d => d.AccommodationAttendeeLogisticSponsorId);

            this.HasOptional(t => t.AirportTransferAttendeeLogisticSponsor)
                .WithMany()
                //.WithMany(e => e.AirportTransferSponsors)
                .HasForeignKey(d => d.AirportTransferAttendeeLogisticSponsorId);

            this.HasOptional(t => t.AirfareAttendeeLogisticSponsor)
                .WithMany()
                //.WithMany(e => e.AirfareSponsors)
                .HasForeignKey(d => d.AirfareAttendeeLogisticSponsorId);

            this.HasRequired(t => t.CreateUser)
                .WithMany()
                .HasForeignKey(d => d.CreateUserId);
        }
    }
}