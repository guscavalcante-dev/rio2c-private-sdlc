// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="PlayerExecutiveBaseApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>PlayerExecutiveBaseApiDto</summary>
    public class PlayerExecutiveBaseApiDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public int? ApiHighlightPosition { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerOrganizationBaseApiDto" /> class.
        /// </summary>
        public PlayerExecutiveBaseApiDto()
        {
        }
    }
}