// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-19-2020
// ***********************************************************************
// <copyright file="AttendeeCollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorDto</summary>
    public class AttendeeCollaboratorDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public IEnumerable<CollaboratorJobTitleBaseDto> JobTitlesDtos { get; set; }
        public IEnumerable<CollaboratorMiniBioBaseDto> MiniBiosDtos { get; set; }
        public IEnumerable<AttendeeOrganizationDto> AttendeeOrganizationsDtos { get; set; }
        public IEnumerable<ConferenceDto> ConferenceDtos { get; set; }
        public IEnumerable<ConferenceParticipantDto> ConferenceParticipantDtos { get; set; }
        public IEnumerable<NegotiationDto> BuyerNegotiationDtos { get; set; }
        public IEnumerable<NegotiationDto> SellerNegotiationDtos { get; set; }
        public Edition Edition { get; set; }

        public LogisticDto LogisticDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorDto"/> class.</summary>
        public AttendeeCollaboratorDto()
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
            return this.Edition?.MusicBusinessRoundMaximumEvaluatorsByProject ?? 0;
        }

        #endregion

        /// <summary>Gets the job title dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public CollaboratorJobTitleBaseDto GetJobTitleDtoByLanguageCode(string culture)
        {
            return this.JobTitlesDtos?.FirstOrDefault(jtd => jtd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the mini bio dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public CollaboratorMiniBioBaseDto GetMiniBioDtoByLanguageCode(string culture)
        {
            return this.MiniBiosDtos?.FirstOrDefault(mbd => mbd.LanguageDto.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }
    }
}