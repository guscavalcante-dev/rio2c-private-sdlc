// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="NegotiationConfigMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>NegotiationConfigMap</summary>
    public class NegotiationConfigMap : EntityTypeConfiguration<NegotiationConfig>
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationConfigMap"/> class.</summary>
        public NegotiationConfigMap()
        {
            this.ToTable("NegotiationConfigs");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.ProjectType)
              .WithMany()
              .HasForeignKey(t => t.ProjectTypeId);
        }
    }
}