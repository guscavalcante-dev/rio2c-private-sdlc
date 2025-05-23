﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="WorkDedicationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>WorkDedicationMap</summary>
    public class WorkDedicationMap : EntityTypeConfiguration<WorkDedication>
    {
        /// <summary>Initializes a new instance of the <see cref="WorkDedicationMap"/> class.</summary>
        public WorkDedicationMap()
        {
            this.ToTable("WorkDedications");

            // Relationships
        }
    }
}