// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-12-2019
// ***********************************************************************
// <copyright file="SalesPlatformIsConsistent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    /// <summary>SalesPlatformIsConsistent</summary>
    public class SalesPlatformIsConsistent : Validation<SalesPlatform>
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformIsConsistent"/> class.</summary>
        public SalesPlatformIsConsistent()
        {
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationPlayerIsRequired(), "Player é obrigatório."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationProjectIsRequired(), "Projeto é obrigatório."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationDateIsRequired(), "Datá é obrigatória."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationRoomIsRequired(), "Sala é obrigatória."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationStartTimeIsRequired(), "Hora de início é obrigatória."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationTableIsRequired(), "Mesa é obrigatória."));            
        }
    }
}
