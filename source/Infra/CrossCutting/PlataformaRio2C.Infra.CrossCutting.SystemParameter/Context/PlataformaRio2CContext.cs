// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SystemParameter
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
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Mapping;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter.Context
{
    /// <summary>PlataformaRio2CContext</summary>
    public class PlataformaRio2CContext : DbContext
    {
        public DbSet<SystemParameter> SystemParameters { get; set; }
        public DbSet<AppAesEncryptionInfo> AppAesEncryptionInfos { get; set; }        

        static PlataformaRio2CContext()
        {
            Database.SetInitializer<PlataformaRio2CContext>(null);
        }

        public PlataformaRio2CContext()
           : base("PlataformaRio2CConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configure Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(p => p.Name == "Id").Configure(p => p.IsKey());

            modelBuilder.Properties().Where(p => p.Name == "Uid")
               .Configure(p => p.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Uid", 2) { IsUnique = true })));            

            modelBuilder.Properties<string>()
               .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(150));

            modelBuilder.Configurations.Add(new SystemParameterMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetupDateRegisterFieldForAllEntities("CreationDate");
            SetupUidRegisterFieldForAllEntities("Uid");

            return base.SaveChanges();
        }

        protected virtual void SetupDateRegisterFieldForAllEntities(string nameDateField)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameDateField).CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameDateField).IsModified = false;
                }
            }
        }

        protected virtual void SetupUidRegisterFieldForAllEntities(string nameDateField)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameDateField).CurrentValue = Guid.NewGuid();
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameDateField).IsModified = false;
                }
            }
        }
    }
}
