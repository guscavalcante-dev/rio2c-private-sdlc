// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="ProducerEventMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProducerEventMap</summary>
    public class ProducerEventMap : EntityTypeConfiguration<ProducerEvent>
    {
        /// <summary>Initializes a new instance of the <see cref="ProducerEventMap"/> class.</summary>
        public ProducerEventMap()
        {
            this.ToTable("ProducerEvent");

            this.Ignore(p => p.Uid);

            this.HasKey(d => new { d.ProducerId, d.EventId });

            //Relationships
            this.HasRequired(t => t.Producer)
              .WithMany()
              .HasForeignKey(d => d.ProducerId);

            this.HasRequired(t => t.Edition)
               .WithMany()
               .HasForeignKey(d => d.EventId);
        }
    }
}