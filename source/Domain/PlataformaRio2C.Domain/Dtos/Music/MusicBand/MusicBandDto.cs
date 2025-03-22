// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-03-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 21-03-2025
// ***********************************************************************
// <copyright file="MusicBandDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandDto</summary>
    public class MusicBandDto
    {
        public MusicBand MusicBand { get; set; }
        public MusicBandType MusicBandType { get; set; }
        public AttendeeMusicBand AttendeeMusicBand { get; set; }
        public AttendeeMusicBandCollaboratorDto AttendeeMusicBandCollaboratorDto { get; set; }
        public AttendeeMusicBandEvaluationDto AttendeeMusicBandEvaluationDto { get; set; }

        public IEnumerable<MusicBandGenreDto> MusicBandGenreDtos { get; set; }
        public IEnumerable<MusicBandTargetAudienceDto> MusicBandTargetAudienceDtos { get; set; }
        public IEnumerable<MusicBandMember> MusicBandMembers { get; set; }
        public IEnumerable<MusicBandTeamMember> MusicBandTeamMembers { get; set; }
        public IEnumerable<ReleasedMusicProject> ReleasedMusicProjects { get; set; }   
        public IEnumerable<AttendeeMusicBandEvaluationDto> AttendeeMusicBandEvaluationsDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBandDto"/> class.</summary>
        public MusicBandDto()
        {
        }

        /// <summary>
        /// Gets the attendee music band evaluation by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public AttendeeMusicBandEvaluationDto GetAttendeeMusicBandEvaluationByUserId(int? userId)
        {
            if (!userId.HasValue)
                return null;

            return this.AttendeeMusicBandEvaluationsDtos.FirstOrDefault(w => w.EvaluatorUser?.Id == userId);
        }

        /// <summary>
        /// Gets the last attendee music band evaluation.
        /// </summary>
        /// <returns></returns>
        public AttendeeMusicBandEvaluationDto GetLastAttendeeMusicBandEvaluation(UserAccessControlDto userAccessControlDto)
        {
            if (userAccessControlDto.IsCommissionMusic())
            {
                return this.AttendeeMusicBandEvaluationsDtos?
                    .OrderByDescending(ambe => ambe.AttendeeMusicBandEvaluation.CommissionEvaluationDate)
                    .FirstOrDefault(ambe => ambe.AttendeeMusicBandEvaluation.CommissionEvaluationDate != null);
            }
            else if (userAccessControlDto.IsCommissionMusicCurator())
            {
                return this.AttendeeMusicBandEvaluationsDtos?
                    .OrderByDescending(ambe => ambe.AttendeeMusicBandEvaluation.CuratorEvaluationDate)
                    .FirstOrDefault(ambe => ambe.AttendeeMusicBandEvaluation.CuratorEvaluationDate != null);

                //TODO: Implements the Repechage logic here
            }
            else
            {
                return null;
            }
        }
    }
}