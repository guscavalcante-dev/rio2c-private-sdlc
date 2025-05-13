// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Gilson Oliveira
// Created          : 29-11-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 29-11-2024
// ***********************************************************************
// <copyright file="CreatePitchingProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreatePitchingProject</summary>
    public class CreatePitchingProject : ProjectBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreatePitchingProject"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="isDataRequired">if set to <c>true</c> [is data required].</param>
        /// <param name="isProductionPlanRequired">if set to <c>true</c> [is production plan required].</param>
        /// <param name="isAdditionalInformationRequired">if set to <c>true</c> [is additional information required].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreatePitchingProject(
            ProjectDto entity,
            List<LanguageDto> languagesDtos,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            bool isDataRequired,
            bool isProductionPlanRequired,
            bool isAdditionalInformationRequired,
            string userInterfaceLanguage
        )
        {
            this.UpdateBaseProperties(
                entity,
                languagesDtos,
                targetAudiences,
                interestsDtos,
                isDataRequired,
                isProductionPlanRequired,
                isAdditionalInformationRequired,
                userInterfaceLanguage,
                false
            );
        }

        /// <summary>Initializes a new instance of the <see cref="CreatePitchingProject"/> class.</summary>
        public CreatePitchingProject()
        {
        }
    }
}