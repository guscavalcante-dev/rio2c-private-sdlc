// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationTechnologyOptionMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InnovationOrganizationTechnologyOptionMap</summary>
    public class InnovationOrganizationTechnologyOptionMap : EntityTypeConfiguration<InnovationOrganizationTechnologyOption>
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationTechnologyOptionMap"/> class.</summary>
        public InnovationOrganizationTechnologyOptionMap()
        {
            this.ToTable("InnovationOrganizationTechnologyOptions");

            // Relationships
        }
    }
}