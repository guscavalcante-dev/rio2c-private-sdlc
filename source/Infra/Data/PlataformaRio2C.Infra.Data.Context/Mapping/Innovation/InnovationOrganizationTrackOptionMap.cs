// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2022
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InnovationOrganizationTrackOptionMap</summary>
    public class InnovationOrganizationTrackOptionMap : EntityTypeConfiguration<InnovationOrganizationTrackOption>
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationTrackOptionMap"/> class.</summary>
        public InnovationOrganizationTrackOptionMap()
        {
            this.ToTable("InnovationOrganizationTrackOptions");

            // Relationships
            this.HasOptional(i => i.InnovationOrganizationTrackOptionGroup)
              .WithMany()
              .HasForeignKey(i => i.InnovationOrganizationTrackOptionGroupId);
        }
    }
}