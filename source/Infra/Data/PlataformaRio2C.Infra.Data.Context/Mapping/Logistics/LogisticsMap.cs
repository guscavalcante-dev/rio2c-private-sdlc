// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="PlataformaRio2CContext.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class LogisticsMap : EntityTypeConfiguration<Logistics>
    {
        public LogisticsMap()
        {


            this.Property(t => t.CollaboratorId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                        new IndexAnnotation(
                                                                new IndexAttribute("IX_Collaborator", 0)
                                                                {
                                                                    IsUnique = true
                                                                }
                                                            )
                                                    )
                .IsRequired();


            this.Property(t => t.EventId)
              .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                      new IndexAnnotation(
                                                              new IndexAttribute("IX_Collaborator", 1)
                                                              {
                                                                  IsUnique = true
                                                              }
                                                          )
                                                  )
              .IsRequired();


            //this.Ignore(m => m.ArrivalTime);
            //this.Ignore(m => m.DepartureTime);

            this.Property(p => p.ArrivalDate)
                //.HasColumnType("DATE")
                .IsRequired();

            this.Property(p => p.ArrivalTime)
                //.HasColumnType("TIME")
              .IsRequired();

            this.Property(p => p.DepartureDate)
                //.HasColumnType("DATE")
                .IsRequired();

            this.Property(p => p.DepartureTime)
                //.HasColumnType("TIME")
              .IsRequired();

            //Relationships
            this.HasRequired(t => t.Collaborator)
               .WithMany()
               .HasForeignKey(d => d.CollaboratorId);


            this.HasRequired(t => t.Edition)
              .WithMany()
              .HasForeignKey(d => d.EventId);

            this.ToTable("Logistics");
        }
    }
}
