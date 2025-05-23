﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="LogisticsSponsorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>
    /// LogisticsSponsorMap
    /// </summary>
    public class LogisticsSponsorMap : EntityTypeConfiguration<LogisticSponsor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogisticsSponsorMap" /> class.
        /// </summary>
        public LogisticsSponsorMap()
        {
            this.ToTable("LogisticSponsors");

            this.Property(t => t.Name)
                .HasMaxLength(LogisticSponsor.NameMaxLength);
        }
    }
}