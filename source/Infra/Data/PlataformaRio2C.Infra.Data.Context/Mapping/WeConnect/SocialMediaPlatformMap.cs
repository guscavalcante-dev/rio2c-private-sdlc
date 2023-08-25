// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 08-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-16-2023
// ***********************************************************************
// <copyright file="SocialMediaPlatformMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>SocialMediaPlatformMap</summary>
    public class SocialMediaPlatformMap : EntityTypeConfiguration<SocialMediaPlatform>
    {
        /// <summary>Initializes a new instance of the <see cref="SocialMediaPlatformMap"/> class.</summary>
        public SocialMediaPlatformMap()
        {
            this.ToTable("SocialMediaPlatforms");
        }
    }
}