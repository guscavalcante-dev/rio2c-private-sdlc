// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ConferenceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceMap</summary>
    public class ConferenceMap : EntityTypeConfiguration<Conference>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceMap"/> class.</summary>
        public ConferenceMap()
        {
            this.ToTable("Conferences");

            //Property(u => u.Info)
            //   .HasMaxLength(Conference.InfoMaxLength);

            //Relationships 
            this.HasOptional(t => t.Room)
              .WithMany()
              .HasForeignKey(d => d.RoomId);
        }
    }
}