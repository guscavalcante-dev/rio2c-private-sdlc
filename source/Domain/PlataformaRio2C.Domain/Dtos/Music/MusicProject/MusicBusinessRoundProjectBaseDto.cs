// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Daniel Giese Rodrigues
// Created          : 03-14-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 03-14-2025
// ***********************************************************************
// <copyright file="MusicBusinessProjectBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundProjectBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string ProjectName { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? FinishDate { get; set; }
    }
}
