// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="OrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorDto</summary>
    public class CollaboratorDto : CollaboratorBaseDto
    {
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }

        public UserBaseDto CreatorDto { get; set; }
        public UserBaseDto UpdaterDto { get; set; }
        public AddressBaseDto AddressBaseDto { get; set; }

        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBiosDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorDto"/> class.</summary>
        public CollaboratorDto()
        {
        }
    }
}