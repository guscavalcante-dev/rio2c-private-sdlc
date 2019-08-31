// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformTicketTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeSalesPlatformTicketTypeMap</summary>
    public class AttendeeSalesPlatformTicketTypeMap : EntityTypeConfiguration<AttendeeSalesPlatformTicketType>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformTicketTypeMap"/> class.</summary>
        public AttendeeSalesPlatformTicketTypeMap()
        {
            this.ToTable("AttendeeSalesPlatformTicketTypes");

            this.Property(t => t.TicketClassId)
                .HasMaxLength(AttendeeSalesPlatformTicketType.TicketClassIdMaxLength);

            this.Property(t => t.TicketClassName)
                .HasMaxLength(AttendeeSalesPlatformTicketType.TicketClassNameMaxLength);

            // Relationships
            this.HasRequired(t => t.AttendeeSalesPlatform)
                .WithMany(e => e.AttendeeSalesPlatformTicketTypes)
                .HasForeignKey(d => d.AttendeeSalesPlatformId);

            this.HasRequired(t => t.TicketType)
                .WithMany(e => e.AttendeeSalesPlatformTicketTypes)
                .HasForeignKey(d => d.TicketTypeId);

            this.HasMany(t => t.AttendeeCollaboratorTickets)
                .WithRequired(e => e.AttendeeSalesPlatformTicketType)
                .HasForeignKey(e => e.AttendeeSalesPlatformTicketTypeId);
        }
    }
}