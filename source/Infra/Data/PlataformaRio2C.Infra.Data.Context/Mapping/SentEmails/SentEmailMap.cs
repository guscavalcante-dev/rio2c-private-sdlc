// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SentEmailMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>SentEmailMap</summary>
    public class SentEmailMap : EntityTypeConfiguration<SentEmail>
    {
        /// <summary>Initializes a new instance of the <see cref="SentEmailMap"/> class.</summary>
        public SentEmailMap()
        {
            this.ToTable("SentEmails");

            this.Property(t => t.EmailType)
                .HasMaxLength(SentEmail.EmailTypeMaxLength)
                .IsRequired();

            this.Ignore(t => t.IsDeleted);
            this.Ignore(t => t.CreateDate);
            this.Ignore(t => t.CreateUserId);
            this.Ignore(t => t.UpdateDate);
            this.Ignore(t => t.UpdateUserId);
        }
    }
}