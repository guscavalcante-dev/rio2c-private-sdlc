
// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Franco
// Created          : 12-01-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 12-01-2022
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationSustainableDevelopmentObjectiveMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationSustainableDevelopmentObjectiveMap</summary>
    public class AttendeeInnovationOrganizationSustainableDevelopmentObjectiveMap : EntityTypeConfiguration<AttendeeInnovationOrganizationSustainableDevelopmentObjective>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationSustainableDevelopmentObjectiveMap"/> class.</summary>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjectiveMap()
        {
            this.ToTable("AttendeeInnovationOrganizationSustainableDevelopmentObjectives");
        }
    }
}