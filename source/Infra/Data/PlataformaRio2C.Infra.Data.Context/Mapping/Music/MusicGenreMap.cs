// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicGenreMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicGenreMap</summary>
    public class MusicGenreMap : EntityTypeConfiguration<MusicGenre>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicGenreMap"/> class.</summary>
        public MusicGenreMap()
        {
            this.ToTable("MusicGenres");

            this.Property(t => t.Name)
              .HasMaxLength(MusicGenre.NameMaxLength);
        }
    }
}