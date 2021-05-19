// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 05-15-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-15-2021
// ***********************************************************************
// <copyright file="UpdateNegotiation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateNegotiation</summary>
    public class UpdateNegotiation : CreateNegotiation
    {
        public Guid NegotiationUid { get; set; }

        public Guid? InitialBuyerOrganizationUid { get; set; }
        public string InitialBuyerOrganizationName { get; set; }

        public Guid? InitialProjectUid { get; set; }
        public string InitialProjectName { get; set; }

        public Guid? InitialNegotiationRoomConfigUid { get; set; }
        public string InitialNegotiationRoomConfigName { get; set; }

        //This "NegotiationConfigUid" does not exists in "Netogiation.cs".
        //This configuration is calculated and saved as DateTime.
        //public Guid? InitialNegotiationConfigUid { get; set; }
        public string InitialDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNegotiation"/> class.
        /// </summary>
        /// <param name="negotiationDto">The negotiation.</param>
        public UpdateNegotiation(NegotiationDto negotiationDto, string userInterfaceLanguage) 
            : base(negotiationDto)
        {
            this.NegotiationUid = negotiationDto?.Negotiation?.Uid ?? Guid.Empty;

            this.InitialBuyerOrganizationUid = negotiationDto?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.Organization?.Uid;
            this.InitialBuyerOrganizationName = negotiationDto?.ProjectBuyerEvaluationDto?.BuyerAttendeeOrganizationDto?.Organization?.TradeName;
            this.InitialProjectUid = negotiationDto?.ProjectBuyerEvaluationDto?.ProjectDto?.Project?.Uid;
            this.InitialProjectName = negotiationDto?.ProjectBuyerEvaluationDto?.ProjectDto?.Project?.ProjectTitles?.FirstOrDefault(pt => pt.Language.Code == userInterfaceLanguage)?.Value;
            
            //this.InitialNegotiationRoomConfigUid = negotiationDto?.Negoti
            this.InitialNegotiationRoomConfigName = negotiationDto?.RoomDto.GetRoomNameByLanguageCode(userInterfaceLanguage)?.RoomName.Value;

            this.InitialDate = negotiationDto?.Negotiation?.StartDate.ToUserTimeZone().Date.ToShortDateString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNegotiation" /> class.
        /// </summary>
        public UpdateNegotiation()
        {
        }
    }
}