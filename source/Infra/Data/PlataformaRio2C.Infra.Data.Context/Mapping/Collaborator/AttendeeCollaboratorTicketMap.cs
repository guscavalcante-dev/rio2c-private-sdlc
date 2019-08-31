// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCollaboratorTicketMap</summary>
    public class AttendeeCollaboratorTicketMap : EntityTypeConfiguration<AttendeeCollaboratorTicket>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformTicketTypeMap"/> class.</summary>
        public AttendeeCollaboratorTicketMap()
        {
            this.ToTable("AttendeeCollaboratorTickets");

            this.Property(t => t.SalesPlatformAttendeeId)
                .HasMaxLength(AttendeeCollaboratorTicket.SalesPlatformAttendeeIdMaxLength);

            this.Property(t => t.FirstName)
                .HasMaxLength(AttendeeCollaboratorTicket.FirstNameMaxLength);

            this.Property(t => t.LastNames)
                .HasMaxLength(AttendeeCollaboratorTicket.LastNamesMaxLength);

            this.Property(t => t.CellPhone)
                .HasMaxLength(AttendeeCollaboratorTicket.CellPhoneMaxLength);

            this.Property(t => t.JobTitle)
                .HasMaxLength(AttendeeCollaboratorTicket.JobTitleMaxLength);

            // Relationships
            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany(e => e.AttendeeCollaboratorTickets)
                .HasForeignKey(d => d.AttendeeCollaboratorId);

            this.HasRequired(t => t.AttendeeSalesPlatformTicketType)
                .WithMany(e => e.AttendeeCollaboratorTickets)
                .HasForeignKey(d => d.AttendeeSalesPlatformTicketTypeId);
        }
    }
}