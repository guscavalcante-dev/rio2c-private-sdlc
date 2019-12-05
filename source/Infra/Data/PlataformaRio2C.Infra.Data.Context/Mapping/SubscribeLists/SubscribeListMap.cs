// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 12-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="SubscribeListMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>SubscribeListMap</summary>
    public class SubscribeListMap : EntityTypeConfiguration<SubscribeList>
    {
        /// <summary>Initializes a new instance of the <see cref="SubscribeListMap"/> class.</summary>
        public SubscribeListMap()
        {
            this.ToTable("SubscribeLists");

            this.HasKey(m => m.Id);

            this.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(SubscribeList.NameMaxLength);

            this.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(SubscribeList.DescriptionMaxLength);

            this.Property(m => m.Code)
                .IsRequired()
                .HasMaxLength(SubscribeList.CodeMaxLength);

            //// Relationships
            //this.HasMany(t => t.SubscribeListUsers)
            //    .WithRequired(e => e.SubscribeList)
            //    .HasForeignKey(e => e.SubscribeListId);
        }
    }
}