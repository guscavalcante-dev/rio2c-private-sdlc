//// ***********************************************************************
//// Assembly         : PlataformaRio2C.Infra.Data
//// Author           : Rafael Dantas Ruiz
//// Created          : 06-19-2019
////
//// Last Modified By : Rafael Dantas Ruiz
//// Last Modified On : 08-06-2019
//// ***********************************************************************
//// <copyright file="ImageFileMap.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    /// <summary>ImageFileMap</summary>
//    public class ImageFileMap : EntityTypeConfiguration<ImageFile>
//    {
//        /// <summary>Initializes a new instance of the <see cref="ImageFileMap"/> class.</summary>
//        public ImageFileMap()
//        {
//            this.ToTable("ImageFile");

//            this.Property(t => t.Uid).IsRequired();
//            this.Property(t => t.CreateDate).IsRequired();
//            this.Property(t => t.File).IsRequired();
//            this.Property(t => t.FileName).HasMaxLength(ImageFile.FileNameMaxLength).IsRequired();
//            this.Property(t => t.ContentType).HasMaxLength(ImageFile.ContentTypeLength).IsRequired();
//            this.Property(t => t.ContentLength).IsRequired();
//        }
//    }
//}