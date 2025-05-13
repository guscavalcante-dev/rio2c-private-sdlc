// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="CollaboratorTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CollaboratorTypeMap</summary>
    public class CollaboratorEditionParticipationMap : EntityTypeConfiguration<CollaboratorEditionParticipation>
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorTypeMap"/> class.</summary>
        public CollaboratorEditionParticipationMap()
        {
            this.ToTable("CollaboratorEditionParticipations");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(e => e.EditionId);


            this.HasRequired(t => t.Collaborator)
                .WithMany(t => t.EditionParticipantions)
                .HasForeignKey(e => e.CollaboratorId);
        }
    }
}