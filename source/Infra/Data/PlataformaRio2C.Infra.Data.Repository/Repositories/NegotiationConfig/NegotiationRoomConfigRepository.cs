// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="NegotiationRoomConfigRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    /// <summary>NegotiationRoomConfigRepository</summary>
    public class NegotiationRoomConfigRepository : Repository<PlataformaRio2CContext, NegotiationRoomConfig>, INegotiationRoomConfigRepository
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationRoomConfigRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public NegotiationRoomConfigRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }       
    }
}
