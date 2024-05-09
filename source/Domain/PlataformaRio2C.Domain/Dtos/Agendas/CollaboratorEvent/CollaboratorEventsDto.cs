using System;
// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 05-09-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-09-2024
// ***********************************************************************
// <copyright file="CollaboratorEventsDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos.Agendas
{
    public class CollaboratorEventsDto
    {
        public Guid CollaboratorUid { get; set; }
        public List<CollaboratorEventDto> CollaboratorEventDtos { get; set; }
    }
}
