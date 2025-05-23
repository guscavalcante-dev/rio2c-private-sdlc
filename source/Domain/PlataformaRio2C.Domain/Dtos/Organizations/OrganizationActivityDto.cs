﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="OrganizationActivityDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationActivityDto</summary>
    public class OrganizationActivityDto
    {
        public int OrganizationActivityId { get; set; }
        public Guid OrganizationActivityUid { get; set; }
        public string OrganizationActivityAdditionalInfo { get; set; }

        public int ActivityId { get; set; }
        public Guid ActivityUid { get; set; }
        public string ActivityName { get; set; }
        public bool ActivityHasAdditionalInfo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationActivityDto"/> class.</summary>
        public OrganizationActivityDto()
        {
        }
    }
}