//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Infra.Data
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="CollaboratorProducerMap.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    /// <summary>CollaboratorProducerMap</summary>
//    public class CollaboratorProducerMap : EntityTypeConfiguration<CollaboratorProducer>
//    {
//        /// <summary>Initializes a new instance of the <see cref="CollaboratorProducerMap"/> class.</summary>
//        public CollaboratorProducerMap()
//        {
//            this.ToTable("CollaboratorProducer");

//            this.Ignore(p => p.Uid);

//            this.HasKey(d => new { d.CollaboratorId, d.ProducerId, d.EventId });

//            //Relationships
//            //this.HasRequired(t => t.Collaborator)
//            //    .WithMany(t => t.ProducersEvents)
//            //    .HasForeignKey(d => d.CollaboratorId);

//            this.HasRequired(t => t.Producer)
//                .WithMany(t => t.EventsCollaborators)
//                .HasForeignKey(d => d.ProducerId);

//            this.HasRequired(t => t.Edition)
//               .WithMany()
//               .HasForeignKey(d => d.EventId);
//        }
//    }
//}