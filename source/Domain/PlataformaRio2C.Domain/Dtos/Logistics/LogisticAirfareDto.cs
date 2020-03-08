// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-20-2020
// ***********************************************************************
// <copyright file="LogisticRequestBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>
    /// Class LogisticRequestBaseDto.
    /// </summary>
    public class LogisticAirfareDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsNational { get; set; }
        public string TicketNumber { get; set; }
        
        public string AdditionalInfo { get; set; }
        public DateTimeOffset DepartureDate { get; set; }
        public DateTimeOffset ArrivalDate { get; set; }
        public DateTimeOffset? TicketUploadDate { get; set; }
    }
}