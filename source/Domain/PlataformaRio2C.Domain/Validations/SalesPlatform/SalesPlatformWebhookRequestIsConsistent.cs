// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="SalesPlatformWebhookRequestIsConsistent.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    /// <summary>SalesPlatformWebhookRequestIsConsistent</summary>
    public class SalesPlatformWebhookRequestIsConsistent : Validation<SalesPlatformWebhookRequest>
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformWebhookRequestIsConsistent"/> class.</summary>
        public SalesPlatformWebhookRequestIsConsistent()
        {
            //base.AddRule(new ValidationRule<SalesPlatformWebhookRequest>(new NegotiationPlayerIsRequired(), "Player é obrigatório."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationProjectIsRequired(), "Projeto é obrigatório."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationDateIsRequired(), "Datá é obrigatória."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationRoomIsRequired(), "Sala é obrigatória."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationStartTimeIsRequired(), "Hora de início é obrigatória."));
            //base.AddRule(new ValidationRule<Negotiation>(new NegotiationTableIsRequired(), "Mesa é obrigatória."));            
        }
    }
}
