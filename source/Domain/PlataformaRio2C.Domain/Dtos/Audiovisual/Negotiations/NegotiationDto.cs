// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="NegotiationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationDto</summary>
    public class NegotiationDto
    {
        public Negotiation Negotiation { get; set; }
        public ProjectBuyerEvaluationDto ProjectBuyerEvaluationDto { get; set; }
        public RoomDto RoomDto { get; set; }
        public UserBaseDto UpdaterDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationDto"/> class.</summary>
        public NegotiationDto()
        {
        }
    }
}