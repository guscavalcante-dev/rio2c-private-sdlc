// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-31-2021
// ***********************************************************************
// <copyright file="MusicProjectJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicProjectJsonDto</summary>
    public class MusicProjectJsonDto
    {
        public int MusicProjectId { get; set; }
        public Guid MusicProjectUid { get; set; }
        public int AttendeeMusicBandId { get; set; }
        public Guid MusicBandUid { get; set; }
        public string MusicBandName { get; set; }
        public string MusicBandTypeName { get; set; }
        public decimal? Grade { get; set; }
        public int EvaluationsCount { get; set; }
        public bool IsApproved { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }

        public IList<string> MusicGenreNames { get; set; }
        public IList<string> MusicTargetAudiencesNames { get; set; }

        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicProjectJsonDto"/> class.</summary>
        public MusicProjectJsonDto()
        {
        }
    }
}