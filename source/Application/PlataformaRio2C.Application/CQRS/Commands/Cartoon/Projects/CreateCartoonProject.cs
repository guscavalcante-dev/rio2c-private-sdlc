// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CreateCartoonProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateCartoonProject</summary>
    public class CreateCartoonProject : BaseCommand
    {
        public int NumberOfEpisodes { get; private set; }
        public string EachEpisodePlayingTime { get; private set; }
        public string TotalValueOfProject { get; private set; }
        public string Title { get; private set; }
        public string Logline { get; private set; }
        public string Summary { get; private set; }
        public string Motivation { get; private set; }
        public string ProductionPlan { get; private set; }
        public string ProjectBibleUrl { get; private set; }
        public string ProjectTeaserUrl { get; private set; }
        public Guid CartoonProjectFormatUid { get; private set; }

        public CartoonProjectCompanyApiDto CartoonProjectCompanyApiDto { get; private set; }
        public List<CartoonProjectCreatorApiDto> CartoonProjectCreatorApiDtos { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCartoonProject"/> class.
        /// </summary>
        /// <param name="numberOfEpisodes">The number of episodes.</param>
        /// <param name="eachEpisodePlayingTime">The each episode playing time.</param>
        /// <param name="totalValueOfProject">The total value of project.</param>
        /// <param name="title">The title.</param>
        /// <param name="logline">The logline.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="motivation">The motivation.</param>
        /// <param name="productionPlan">The production plan.</param>
        /// <param name="projectBibleUrl">The project bible URL.</param>
        /// <param name="projectTeaserUrl">The project teaser URL.</param>
        /// <param name="cartoonProjectFormatUid">The cartoon project format uid.</param>
        /// <param name="cartoonProjectCompanyApiDto">The cartoon project company API dto.</param>
        /// <param name="cartoonProjectCreatorApiDtos">The cartoon project creator API dtos.</param>
        public CreateCartoonProject(
            int numberOfEpisodes,
            string eachEpisodePlayingTime,
            string totalValueOfProject,
            string title,
            string logline,
            string summary,
            string motivation,
            string productionPlan,
            string projectBibleUrl,
            string projectTeaserUrl,
            Guid cartoonProjectFormatUid,
            CartoonProjectCompanyApiDto cartoonProjectCompanyApiDto,
            List<CartoonProjectCreatorApiDto> cartoonProjectCreatorApiDtos)
        {
            this.NumberOfEpisodes = numberOfEpisodes;
            this.EachEpisodePlayingTime = eachEpisodePlayingTime;
            this.TotalValueOfProject = totalValueOfProject;
            this.Title = title;
            this.Logline = logline;
            this.Summary = summary;
            this.Motivation = motivation;
            this.ProductionPlan = productionPlan;
            this.ProjectBibleUrl = projectBibleUrl;
            this.ProjectTeaserUrl = projectTeaserUrl;
            this.CartoonProjectFormatUid = cartoonProjectFormatUid;
            this.CartoonProjectCompanyApiDto = cartoonProjectCompanyApiDto;
            this.CartoonProjectCreatorApiDtos = cartoonProjectCreatorApiDtos;
        }
    }
}