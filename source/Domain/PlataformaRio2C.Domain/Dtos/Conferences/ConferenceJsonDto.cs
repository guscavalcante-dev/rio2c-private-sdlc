// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="ConferenceJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceJsonDto</summary>
    public class ConferenceJsonDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public EditionEventJsonDto EditionEventJsonDto { get; set; }
        public RoomJsonDto RoomJsonDto { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceJsonDto"/> class.</summary>
        public ConferenceJsonDto()
        {
        }
    }
}