// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-12-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="LogisticDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>LogisticDto</summary>
    public class LogisticDto
    {
        public Logistic Logistic { get; set; }
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public AttendeeLogisticSponsorDto AirfareAttendeeLogisticSponsorDto { get; set; }
        public AttendeeLogisticSponsorDto AccommodationAttendeeLogisticSponsorDto { get; set; }
        public AttendeeLogisticSponsorDto AirportTransferAttendeeLogisticSponsorDto { get; set; }
        public UserDto CreateUserDto { get; set; }

        public IEnumerable<LogisticAirfareDto> LogisticAirfareDtos { get; set; }
        public IEnumerable<LogisticAccommodationDto> LogisticAccommodationDtos { get; set; }
        public IEnumerable<LogisticTransferDto> LogisticTransferDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticDto"/> class.</summary>
        public LogisticDto()
        {
        }
    }
}