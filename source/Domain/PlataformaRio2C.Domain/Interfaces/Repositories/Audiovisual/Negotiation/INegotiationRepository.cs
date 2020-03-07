// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="INegotiationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>INegotiationRepository</summary>
    public interface INegotiationRepository : IRepository<Negotiation>
    {
        //IEnumerable<Player> GetAllPlayers();
        //IEnumerable<Producer> GetAllProducers();
        //IQueryable<Negotiation> GetAllBySchedule(Expression<Func<Negotiation, bool>> filter);        
        void Truncate();
    }    
}