// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="ActivityMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ActivityMap</summary>
    public class ActivityMap : EntityTypeConfiguration<Activity>
    {
        /// <summary>Initializes a new instance of the <see cref="ActivityMap"/> class.</summary>
        public ActivityMap()
        {
            this.ToTable("Activities");

            //Relationships
            this.HasRequired(t => t.ProjectType)
                .WithMany(e => e.Activities)
                .HasForeignKey(d => d.ProjectTypeId);
        }
    }
}