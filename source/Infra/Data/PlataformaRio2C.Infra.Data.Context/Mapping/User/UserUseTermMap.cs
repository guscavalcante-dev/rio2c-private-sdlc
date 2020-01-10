//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Infra.Data
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="UserUseTermMap.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    /// <summary>UserUseTermMap</summary>
//    public class UserUseTermMap : EntityTypeConfiguration<UserUseTerm>
//    {
//        /// <summary>Initializes a new instance of the <see cref="UserUseTermMap"/> class.</summary>
//        public UserUseTermMap()
//        {
//            this.ToTable("UserUseTerm");

//            //Relationships          
//            this.HasRequired(t => t.User)
//                .WithMany(t => t.UserUseTerms)
//                .HasForeignKey(d => d.UserId);

//            this.HasRequired(t => t.Edition)
//                .WithMany()
//                .HasForeignKey(d => d.EventId);

//            this.HasRequired(t => t.Role)
//                .WithMany()
//                .HasForeignKey(d => d.RoleId);
//        }
//    }
//}