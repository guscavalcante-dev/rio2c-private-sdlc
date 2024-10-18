// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="AgendaBaseEventJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AgendaBaseEventJsonDto</summary>
    public class AgendaBaseEventJsonDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? Start { get; set; }
        public DateTimeOffset? End { get; set; }
        public bool AllDay { get; set; }
        public string Css { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaBaseEventJsonDto"/> class.</summary>
        public AgendaBaseEventJsonDto()
        {
        }
    }
}