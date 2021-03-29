// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="TargetAudienceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>TargetAudienceMap</summary>
    public class TargetAudienceMap : EntityTypeConfiguration<TargetAudience>
    {
        /// <summary>Initializes a new instance of the <see cref="TargetAudienceMap"/> class.</summary>
        public TargetAudienceMap()
        {
            this.ToTable("TargetAudiences");

            // Relationships
            this.HasRequired(ta => ta.ProjectType)
                .WithMany()
                .HasForeignKey(ta => ta.ProjectTypeId);
        }
    }
}
