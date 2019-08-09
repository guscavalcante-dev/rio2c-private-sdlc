// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="LanguageMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>LanguageMap</summary>
    public class LanguageMap : EntityTypeConfiguration<Language>
    {
        /// <summary>Initializes a new instance of the <see cref="LanguageMap"/> class.</summary>
        public LanguageMap()
        {
            this.ToTable("Languages");

            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Code).IsRequired();
        }
    }
}