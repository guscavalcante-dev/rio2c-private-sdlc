// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-11-2024
// ***********************************************************************
// <copyright file="AttendeeOrganizationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeOrganizationDto</summary>
    public class AttendeeOrganizationDto
    {
        public AttendeeOrganization AttendeeOrganization { get; set; }
        public Organization Organization { get; set; }
        public Edition Edition { get; set; }
        public IEnumerable<AttendeeCollaboratorDto> AttendeeCollaboratorDtos { get; set; }
        public int ProjectBuyerEvaluationsCount { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationDto"/> class.</summary>
        public AttendeeOrganizationDto()
        {
        }

        #region Edition Limits

        /// <summary>Gets the maximum sell projects count.</summary>
        /// <returns></returns>
        public int GetMaxSellProjectsCount()
        {
            return this.Edition?.AttendeeOrganizationMaxSellProjectsCount ?? 0;
        }

        /// <summary>Gets the project maximum buyer evaluations count.</summary>
        /// <returns></returns>
        public int GetProjectMaxBuyerEvaluationsCount()
        {
            return this.Edition?.ProjectMaxBuyerEvaluationsCount ?? 0;
        }

        #endregion

        public double GetDemandPercentage(int projectsCount = 0)
        {
            return (double) projectsCount > 0
                ? Math.Round((double)this.ProjectBuyerEvaluationsCount / projectsCount * 100, 2)
                : 0;
        }

        public List<string> GetDemandLabel(int projectsCount = 0)
        {
            var percentage = this.GetDemandPercentage(projectsCount);
            var demand = new List<string>();
            if (percentage <= 5)
            {
                demand.Add(Labels.Low);
                demand.Add("text-success");
                demand.Add("bg-success");
                return demand;
            }
            if (percentage <= 10)
            {
                demand.Add(Labels.Medium);
                demand.Add("text-warning");
                demand.Add("bg-warning");
                return demand;
            }
            demand.Add(Labels.High);
            demand.Add("text-danger");
            demand.Add("bg-danger");
            return demand;
        }
    }
}