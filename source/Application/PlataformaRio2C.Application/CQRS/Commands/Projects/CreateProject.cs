// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-07-2019
// ***********************************************************************
// <copyright file="CreateProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateProject</summary>
    public class CreateProject : ProjectBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateProject"/> class.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        public CreateProject(
            List<LanguageDto> languagesDtos, 
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<IGrouping<InterestGroup, Interest>> groupedInterests,
            bool isDataRequired)
        {
            this.UpdateBaseProperties(
                null,
                languagesDtos, 
                activities, 
                targetAudiences, 
                groupedInterests,
                isDataRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateProject"/> class.</summary>
        public CreateProject()
        {
        }
    }
}