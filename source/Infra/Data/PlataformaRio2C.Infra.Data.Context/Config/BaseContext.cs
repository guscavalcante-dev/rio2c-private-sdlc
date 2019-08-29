// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-27-2019
// ***********************************************************************
// <copyright file="BaseContext.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Context.Config
{
    /// <summary>BaseContext</summary>
    public class BaseContext : DbContext, IDbContext
    {
        /// <summary>Initializes a new instance of the <see cref="BaseContext"/> class.</summary>
        /// <param name="connectionString">The connection string.</param>
        public BaseContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configure Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(p => p.Name == "Id").Configure(p => p.IsKey());
            modelBuilder.Types().Configure(c => c.Ignore("ValidationResult"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(50));

            modelBuilder.Properties().Where(p => p.Name == "Uid")
                .Configure(p => p.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Uid", 2) { IsUnique = true })));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            //SetupDateRegisterFieldForAllEntities("CreationDate");
            SetupUidRegisterFieldForAllEntities("Uid");
            SetupUidStringRegisterFieldForAllEntities("SecurityStamp");
            return base.SaveChanges();
        }

        [LogConfig(NoLog = true)]
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

        [LogConfig(NoLog = true)]
        protected virtual void SetupUidRegisterFieldForAllEntities(string nameDateField)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameDateField).CurrentValue = Guid.NewGuid();
                }

                if (entry.State == EntityState.Modified && entry.OriginalValues.PropertyNames.Any(e => e == nameDateField))
                {
                    entry.Property(nameDateField).IsModified = false;                    
                }
            }
        }

        [LogConfig(NoLog = true)]
        protected virtual void SetupUidStringRegisterFieldForAllEntities(string nameDateField)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(nameDateField) != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameDateField).CurrentValue = Guid.NewGuid().ToString();
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameDateField).IsModified = false;
                }
            }
        }

    }
}
