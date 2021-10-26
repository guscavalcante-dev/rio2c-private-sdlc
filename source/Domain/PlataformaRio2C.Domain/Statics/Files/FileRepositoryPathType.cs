// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2021
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
        public static FileRepositoryPathType MusicBandImage = new FileRepositoryPathType(new Guid("1BAD7977-5B6A-47F6-9484-6D56379A8ADC"));
        public static FileRepositoryPathType UserImage = new FileRepositoryPathType(new Guid("A94034FA-4648-4B27-B3BE-F88F172E475D"));
        public static FileRepositoryPathType LogisticAirfareFile = new FileRepositoryPathType(new Guid("90DD5AB8-1210-4278-96EC-FFB414535CB4"));
        public static FileRepositoryPathType InnovationOrganizationPresentationFile = new FileRepositoryPathType(new Guid("319ECD0E-9B1B-4563-B98A-C09D59453EF3"));
        public static FileRepositoryPathType AudioFile = new FileRepositoryPathType(new Guid("14EF53C3-C034-487F-8C08-A467167AD286"));
    }
}