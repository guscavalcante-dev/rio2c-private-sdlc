// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SystemParameter
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="SystemParameterMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter.Mapping
{
    /// <summary>SystemParameterMap</summary>
    public class SystemParameterMap : EntityTypeConfiguration<SystemParameter>
    {
        /// <summary>Initializes a new instance of the <see cref="SystemParameterMap"/> class.</summary>
        public SystemParameterMap()
        {
            this.ToTable("SystemParameters");

            HasKey(t => t.Id);

            this.Property(t => t.Code)
                 .HasColumnAnnotation(
                        IndexAnnotation.AnnotationName,
                        new IndexAnnotation(new IndexAttribute())
                  );

            this.Property(t => t.Description)
                .HasMaxLength(SystemParameter.DescriptionMaxLength);

            this.Property(t => t.Value)
                .HasMaxLength(SystemParameter.ValueMaxLength);
        }
    }
}