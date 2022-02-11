// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 02-08-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-08-2022
// ***********************************************************************
// <copyright file="CartoonProjectDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CartoonProjectDto</summary>
    public class CartoonProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LogLine { get; set; }
        public string Summary { get; set; }
        public string Motivation { get; set; }
        public string NumberOfEpisodes { get; set; }
        public string EachEpisodePlayingTime { get; set; }
        public string TotalValueOfProject { get; set; }
        public string CartoonProjectFormatName { get; set; }

        //TODO: Implement this at database! We receive in payload but doesn't saving at database.
        public string ProductionPlan { get; set; }
        public string ProjectBibleUrl { get; set; }
        public string ProjectTeaserUrl { get; set; }

        public IEnumerable<CartoonProjectCreator> Creators { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CartoonProjectDto"/> class.</summary>
        public CartoonProjectDto()
        {
        }
    }
}