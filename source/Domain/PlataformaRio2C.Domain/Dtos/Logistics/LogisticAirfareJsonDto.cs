// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticAirfareJsonDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>
    /// Class LogisticAirfareJsonDto.
    /// </summary>
    public class LogisticAirfareJsonDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsNational { get; set; }
        public bool IsArrival { get; set; }
        public string TicketNumber { get; set; }
        
        public string AdditionalInfo { get; set; }
        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public DateTimeOffset? TicketUploadDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
    }
}