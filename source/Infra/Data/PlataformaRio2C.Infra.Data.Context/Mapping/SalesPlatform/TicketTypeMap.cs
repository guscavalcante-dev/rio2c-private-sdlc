// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="TicketTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>TicketTypeMap</summary>
    public class TicketTypeMap : EntityTypeConfiguration<TicketType>
    {
        /// <summary>Initializes a new instance of the <see cref="TicketTypeMap"/> class.</summary>
        public TicketTypeMap()
        {
            this.ToTable("TicketTypes");

            this.Property(t => t.Name)
                .HasMaxLength(TicketType.NameMaxLength);

            this.Property(t => t.Code)
                .HasMaxLength(TicketType.CodeMaxLength);

            // Relationships
            this.HasMany(t => t.AttendeeSalesPlatformTicketTypes)
                .WithRequired(e => e.TicketType)
                .HasForeignKey(e => e.TicketTypeId);
        }
    }
}