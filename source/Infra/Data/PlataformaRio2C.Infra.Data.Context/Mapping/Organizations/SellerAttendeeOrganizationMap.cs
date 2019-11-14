// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 11-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-14-2019
// ***********************************************************************
// <copyright file="SellerAttendeeOrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>SellerAttendeeOrganizationMap</summary>
    public class SellerAttendeeOrganizationMap : EntityTypeConfiguration<SellerAttendeeOrganization>
    {
        /// <summary>Initializes a new instance of the <see cref="SellerAttendeeOrganizationMap"/> class.</summary>
        public SellerAttendeeOrganizationMap()
        {
            this.ToTable("SellerAttendeeOrganizations");

            // Relationships
            this.HasRequired(t => t.AttendeeOrganization)
                .WithMany(e => e.SellerAttendeeOrganizations)
                .HasForeignKey(d => d.AttendeeOrganizationId);

            this.HasRequired(t => t.AttendeeCollaboratorTicket)
                .WithMany()
                .HasForeignKey(d => d.AttendeeCollaboratorTicketId);
        }
    }
}