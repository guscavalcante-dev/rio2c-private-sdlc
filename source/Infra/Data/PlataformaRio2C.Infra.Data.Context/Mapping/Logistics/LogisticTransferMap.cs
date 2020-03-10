// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticTransferMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>
    /// LogisticTransferMap
    /// </summary>
    public class LogisticTransferMap : EntityTypeConfiguration<LogisticTransfer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogisticTransferMap" /> class.
        /// </summary>
        public LogisticTransferMap()
        {
            this.ToTable("LogisticTransfers");

            // Relationships
            this.HasRequired(e => e.Logistic)
                .WithMany(e => e.LogisticTransfers)
                .HasForeignKey(e => e.LogisticId);
        }
    }
}