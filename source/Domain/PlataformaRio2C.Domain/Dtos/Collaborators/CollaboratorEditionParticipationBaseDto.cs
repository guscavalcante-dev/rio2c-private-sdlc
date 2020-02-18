// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="CollaboratorMiniBioBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorMiniBioBaseDto</summary>
    public class CollaboratorEditionParticipationBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public Guid EditionUid { get; set; }
        public int EditionId { get; set; }
        public string EditionName { get; set; }
        
        /// <summary>Initializes a new instance of the <see cref="CollaboratorMiniBioBaseDto"/> class.</summary>
        public CollaboratorEditionParticipationBaseDto()
        {
        }
    }
}