// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-21-2019
// ***********************************************************************
// <copyright file="MessageMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MessageMap</summary>
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            this.ToTable("Messages");

            this.Property(u => u.Text)
                //.HasColumnType("nvarchar(max)")
                .HasMaxLength(Message.TextMaxLength);

            this.Ignore(m => m.IsDeleted);
            this.Ignore(m => m.CreateDate);
            this.Ignore(m => m.CreateUserId);
            this.Ignore(m => m.UpdateDate);
            this.Ignore(m => m.UpdateUserId);

            //Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.Sender)
                .WithMany()
                .HasForeignKey(d => d.SenderId);

            this.HasRequired(t => t.Recipient)
                .WithMany()
                .HasForeignKey(d => d.RecipientId);
        }
    }
}