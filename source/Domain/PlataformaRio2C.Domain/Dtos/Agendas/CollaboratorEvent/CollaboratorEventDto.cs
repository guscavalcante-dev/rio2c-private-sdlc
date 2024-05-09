// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 05-09-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-09-2024
// ***********************************************************************
// <copyright file="CollaboratorEventDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos.Agendas
{
    public class CollaboratorEventDto
    {
        public string Local { get; set; }
        public string Horario { get; set; }
        public DateTime? Data { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
    }
}
