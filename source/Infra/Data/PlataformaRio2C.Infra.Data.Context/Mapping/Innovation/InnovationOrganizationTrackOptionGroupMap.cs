// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 12-27-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2022
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionGroupMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InnovationOrganizationTrackOptionGroupMap</summary>
    public class InnovationOrganizationTrackOptionGroupMap : EntityTypeConfiguration<InnovationOrganizationTrackOptionGroup>
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionGroupMap"/> class.</summary>
        public InnovationOrganizationTrackOptionGroupMap()
        {
            this.ToTable("InnovationOrganizationTrackOptionGroups");

            // Relationships
            this.HasMany(g => g.InnovationOrganizationTrackOptions)
                .WithOptional(e => e.InnovationOrganizationTrackOptionGroup)
                .HasForeignKey(e => e.InnovationOrganizationTrackOptionGroupId);
        }
    }
}