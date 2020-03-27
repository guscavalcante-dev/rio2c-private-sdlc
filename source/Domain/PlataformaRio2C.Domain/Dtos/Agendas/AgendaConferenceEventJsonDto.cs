// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="AgendaConferenceEventJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AgendaConferenceEventJsonDto</summary>
    public class AgendaConferenceEventJsonDto : AgendaBaseEventJsonDto
    {
        public string EditionEvent { get; set; }
        public string Synopsis { get; set; }
        public string Room { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AgendaConferenceEventJsonDto"/> class.</summary>
        public AgendaConferenceEventJsonDto()
        {
        }
    }
}