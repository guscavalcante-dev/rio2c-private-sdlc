// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferencePresentationFormatMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferencePresentationFormatMap</summary>
    public class ConferencePresentationFormatMap : EntityTypeConfiguration<ConferencePresentationFormat>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferencePresentationFormatMap"/> class.</summary>
        public ConferencePresentationFormatMap()
        {
            this.ToTable("ConferencePresentationFormats");

            //Relationships 
            this.HasRequired(t => t.Conference)
              .WithMany(d => d.ConferencePresentationFormats)
              .HasForeignKey(t => t.ConferenceId);

            this.HasRequired(t => t.PresentationFormat)
                .WithMany(d => d.ConferencePresentationFormats)
                .HasForeignKey(t => t.PresentationFormatId);
        }
    }
}