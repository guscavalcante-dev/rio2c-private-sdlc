// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="HoldingListDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>HoldingListDto</summary>
    public class HoldingListDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public bool IsImageUploaded { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdatedDto { get; set; }
        public IEnumerable<HoldingDescriptionBaseDto> DescriptionsDtos { get; set; }


        /// <summary>Initializes a new instance of the <see cref="HoldingListDto"/> class.</summary>
        public HoldingListDto()
        {
        }
    }
}