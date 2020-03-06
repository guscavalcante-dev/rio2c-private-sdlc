// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="PlataformaRio2CContext.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class LogisticsMap : EntityTypeConfiguration<Logistics>
    {
        public LogisticsMap()
        {
            this.HasOptional(t => t.AccommodationSponsor)
                .WithMany(e => e.AccommodationSponsors)
              .HasForeignKey(d => d.AccommodationAttendeeLogisticSponsorId);

            this.HasOptional(t => t.AirportTransferSponsor)
                .WithMany(e => e.AirportTransferSponsors)
                .HasForeignKey(d => d.AirportTransferAttendeeLogisticSponsorId);

            this.HasOptional(t => t.AirfareSponsor)
                .WithMany(e => e.AirfareSponsors)
                .HasForeignKey(d => d.AirfareAttendeeLogisticSponsorId);

            this.HasRequired(t => t.CreateUser)
                .WithMany()
                .HasForeignKey(d => d.CreateUserId);


            ToTable("Logistics");
        }
    }
}