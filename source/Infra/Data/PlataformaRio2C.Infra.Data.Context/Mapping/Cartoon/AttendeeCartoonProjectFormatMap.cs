// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectFormatMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCartoonProjectFormatMap</summary>
    public class AttendeeCartoonProjectFormatMap : EntityTypeConfiguration<AttendeeCartoonProjectFormat>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectFormatMap"/> class.</summary>
        public AttendeeCartoonProjectFormatMap()
        {
            this.ToTable("AttendeeCartoonProjectFormats");

            // Relationships
            this.HasRequired(t => t.CartoonProject)
                .WithMany(aio => aio.AttendeeCartoonProjectFormats)
                .HasForeignKey(d => d.CartoonProjectId);

            this.HasRequired(t => t.CartoonProjectFormat)
                .WithMany()
                .HasForeignKey(d => d.CartoonProjectFormatId);
        }
    }
}