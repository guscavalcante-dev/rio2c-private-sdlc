// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-15-2019
// ***********************************************************************
// <copyright file="FileRepositoryPathType.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Statics
{
    /// <summary>FileRepositoryPathType</summary>
    public class FileRepositoryPathType
    {
        public Guid Uid { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FileRepositoryPathType"/> class.</summary>
        /// <param name="uid">The uid.</param>
        public FileRepositoryPathType(Guid uid)
        {
            this.Uid = uid;
        }

        public static FileRepositoryPathType HoldingImage = new FileRepositoryPathType(new Guid("307633BA-9475-45BA-9B86-80E6DA423F2F"));
        public static FileRepositoryPathType OrganizationImage = new FileRepositoryPathType(new Guid("77A64959-117B-4FCC-B031-B519C0F65196"));
        public static FileRepositoryPathType UserImage = new FileRepositoryPathType(new Guid("A94034FA-4648-4B27-B3BE-F88F172E475D"));
    }
}