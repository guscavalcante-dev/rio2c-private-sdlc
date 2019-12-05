// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 12-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="UserUnsubscribedListMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>UserUnsubscribedListMap</summary>
    public class UserUnsubscribedListMap : EntityTypeConfiguration<UserUnsubscribedList>
    {
        /// <summary>Initializes a new instance of the <see cref="UserUnsubscribedListMap"/> class.</summary>
        public UserUnsubscribedListMap()
        {
            this.ToTable("UserUnsubscribedLists");

            this.HasKey(m => m.Id);

            // Relationships
            this.HasRequired(t => t.SubscribeList)
                .WithMany()
                .HasForeignKey(d => d.SubscribeListId);

            this.HasRequired(t => t.User)
                .WithMany(e => e.UserUnsubscribedLists)
                .HasForeignKey(d => d.UserId);
        }
    }
}