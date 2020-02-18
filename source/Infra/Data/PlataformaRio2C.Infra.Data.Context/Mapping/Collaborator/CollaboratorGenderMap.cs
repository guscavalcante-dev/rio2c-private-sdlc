// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-16-2019
// ***********************************************************************
// <copyright file="CollaboratorTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CollaboratorTypeMap</summary>
    public class CollaboratorGenderMap : EntityTypeConfiguration<CollaboratorGender>
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorTypeMap"/> class.</summary>
        public CollaboratorGenderMap()
        {
            this.ToTable("CollaboratorGenders");

            this.Property(t => t.Name)
                .HasMaxLength(CollaboratorGender.NameMaxLength);

            // Relationships            
        }
    }
}