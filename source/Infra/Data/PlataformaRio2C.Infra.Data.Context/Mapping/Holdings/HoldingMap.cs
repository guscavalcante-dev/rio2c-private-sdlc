// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>HoldingMap</summary>
    public class HoldingMap : EntityTypeConfiguration<Holding>
    {
        /// <summary>Initializes a new instance of the <see cref="HoldingMap"/> class.</summary>
        public HoldingMap()
        {
            this.ToTable("Holdings");

            this.Property(t => t.Name)
                .HasMaxLength(Holding.NameMaxLength)
                //.HasColumnAnnotation(IndexAnnotation.AnnotationName,
                //                        new IndexAnnotation(
                //                                                new IndexAttribute("IX_Name", 2) {
                //                                                    IsUnique = true
                //                                                }
                //                                            )
                //                                    )
                .IsRequired();

            //Relationships
            //this.HasOptional(t => t.Image)
            //    .WithMany()
            //    .HasForeignKey(d => d.ImageId);
        }
    }
}