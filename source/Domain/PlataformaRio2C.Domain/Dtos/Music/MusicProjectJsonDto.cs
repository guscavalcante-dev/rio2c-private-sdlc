// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-01-2020
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
        public string MusicBandName { get; set; }
        public string MusicBandImageUrl { get; set; }
        public string MusicBandTypeName { get; set; }
        public string EvaluationStatusName { get; set; }

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