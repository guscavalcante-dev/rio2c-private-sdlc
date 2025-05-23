﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 28-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-08-2024
// ***********************************************************************
// <copyright file="CreateInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateInnovationOrganization</summary>
    public class CreateInnovationOrganization : BaseCommand
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string ServiceName { get; private set; }
        public DateTime FoundationDate { get; private set; }
        public decimal AccumulatedRevenue { get; private set; }
        public string Description { get; private set; }
        public string BusinessDefinition { get; private set; }
        public string Website { get; private set; }
        public string BusinessFocus { get; private set; }
        public string MarketSize { get; private set; }
        public string BusinessEconomicModel { get; private set; }
        public string BusinessOperationalModel { get; private set; }
        public string ImageFile { get; private set; }
        public string VideoUrl { get; private set; }
        public string BusinessDifferentials { get; private set; }
        public string BusinessStage { get; private set; }
        public string ResponsibleName { get; private set; }
        public string Email { get; private set; }
        public string CellPhone { get; private set; }
        public string PresentationFile { get; private set; }
        public string PresentationFileName { get; private set; }

        public bool? WouldYouLikeParticipateBusinessRound { get; private set; }
        public bool? WouldYouLikeParticipatePitching { get; private set; }
        public decimal? AccumulatedRevenueForLastTwelveMonths { get; private set; }
        public int? FoundationYear { get; private set; }

        public List<AttendeeInnovationOrganizationFounderApiDto> AttendeeInnovationOrganizationFounderApiDtos { get; set; }
        public List<AttendeeInnovationOrganizationCompetitorApiDto> AttendeeInnovationOrganizationCompetitorApiDtos { get; set; }
        public List<InnovationOrganizationExperienceOptionApiDto> InnovationOrganizationExperienceOptionApiDtos { get; set; }
        public List<InnovationOrganizationTrackOptionApiDto> InnovationOrganizationTrackOptionApiDtos { get; set; }
        public List<InnovationOrganizationObjectivesOptionApiDto> InnovationOrganizationObjectivesOptionApiDtos { get; set; }
        public List<InnovationOrganizationTechnologyOptionApiDto> InnovationOrganizationTechnologyOptionApiDtos { get; set; }
        public List<InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto> InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganization" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="document">The document.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="foundationDate">The foundation date.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="description">The description.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="website">The website.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="imageFile">The image file.</param>
        /// <param name="videoUrl">The video URL.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="responsibleName">Name of the responsible.</param>
        /// <param name="email">The email.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="presentationFile">The presentation file.</param>
        /// <param name="presentationFileName">Name of the presentation file.</param>
        /// <param name="attendeeInnovationOrganizationFounderApiDtos">The attendee innovation organization founder API dtos.</param>
        /// <param name="attendeeInnovationOrganizationCompetitorApiDtos">The attendee innovation organization competitor API dtos.</param>
        /// <param name="innovationOrganizationExperienceOptionApiDtos">The innovation organization experience option API dtos.</param>
        /// <param name="innovationOrganizationTrackOptionApiDtos">The innovation organization track option API dtos.</param>
        /// <param name="innovationOrganizationObjectivesOptionApiDtos">The innovation organization objectives option API dtos.</param>
        /// <param name="innovationOrganizationTechnologyOptionApiDtos">The innovation organization technology option API dtos.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos">The innovation organization sustainable development objectives option API dtos.</param>
        /// <param name="wouldYouLikeParticipateBusinessRound">would you like participate business round.</param>
        /// <param name="wouldYouLikeParticipatePitching">The would you like participate pitching.</param>
        /// <param name="accumulatedRevenueForLastTwelveMonths">accumulated revenue for last twelve months.</param>
        /// <param name="businessFoundationYear">business foundtaion year.</param>
        public CreateInnovationOrganization(
            string name,
            string document,
            string serviceName,
            DateTime foundationDate,
            decimal accumulatedRevenue,
            string description,
            string businessDefinition,
            string website,
            string businessFocus,
            string marketSize,
            string businessEconomicModel,
            string businessOperationalModel,
            string imageFile,
            string videoUrl,
            string businessDifferentials,
            string businessStage,
            string responsibleName,
            string email,
            string cellPhone,
            string presentationFile,
            string presentationFileName,
            List<AttendeeInnovationOrganizationFounderApiDto> attendeeInnovationOrganizationFounderApiDtos,
            List<AttendeeInnovationOrganizationCompetitorApiDto> attendeeInnovationOrganizationCompetitorApiDtos,
            List<InnovationOrganizationExperienceOptionApiDto> innovationOrganizationExperienceOptionApiDtos,
            List<InnovationOrganizationTrackOptionApiDto> innovationOrganizationTrackOptionApiDtos,
            List<InnovationOrganizationObjectivesOptionApiDto> innovationOrganizationObjectivesOptionApiDtos,
            List<InnovationOrganizationTechnologyOptionApiDto> innovationOrganizationTechnologyOptionApiDtos,
            List<InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto> innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos,
            bool? wouldYouLikeParticipateBusinessRound,
            bool? wouldYouLikeParticipatePitching,
            decimal? accumulatedRevenueForLastTwelveMonths,
            int? businessFoundationYear
            )
        {
            this.Name = name;
            this.Document = document;
            this.ServiceName = serviceName;
            this.FoundationDate = foundationDate;
            this.AccumulatedRevenue = accumulatedRevenue;
            this.Description = description;
            this.BusinessDefinition = businessDefinition;
            this.Website = website;
            this.BusinessFocus = businessFocus;
            this.MarketSize = marketSize;
            this.BusinessEconomicModel = businessEconomicModel;
            this.BusinessOperationalModel = businessOperationalModel;
            this.ImageFile = imageFile;
            this.VideoUrl = videoUrl;
            this.BusinessDifferentials = businessDifferentials;
            this.BusinessStage = businessStage;
            this.ResponsibleName = responsibleName;
            this.Email = email;
            this.CellPhone = cellPhone;
            this.PresentationFile = presentationFile;
            this.PresentationFileName = presentationFileName;

            this.AttendeeInnovationOrganizationFounderApiDtos = attendeeInnovationOrganizationFounderApiDtos;
            this.AttendeeInnovationOrganizationCompetitorApiDtos = attendeeInnovationOrganizationCompetitorApiDtos;
            this.InnovationOrganizationExperienceOptionApiDtos = innovationOrganizationExperienceOptionApiDtos;
            this.InnovationOrganizationTrackOptionApiDtos = innovationOrganizationTrackOptionApiDtos;
            this.InnovationOrganizationObjectivesOptionApiDtos = innovationOrganizationObjectivesOptionApiDtos;
            this.InnovationOrganizationTechnologyOptionApiDtos = innovationOrganizationTechnologyOptionApiDtos;
            this.InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos = innovationOrganizationSustainableDevelopmentObjectivesOptionApiDtos;

            this.WouldYouLikeParticipateBusinessRound = wouldYouLikeParticipateBusinessRound;
            this.WouldYouLikeParticipatePitching = wouldYouLikeParticipatePitching;
            this.AccumulatedRevenueForLastTwelveMonths = accumulatedRevenueForLastTwelveMonths;
            this.FoundationYear = businessFoundationYear;
        }
    }
}