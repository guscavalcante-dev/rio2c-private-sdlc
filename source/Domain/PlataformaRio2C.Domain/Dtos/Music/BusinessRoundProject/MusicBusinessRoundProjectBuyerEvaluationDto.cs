using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundProjectBuyerEvaluationDto
    {
        public MusicBusinessRoundProjectBuyerEvaluation MusicBusinessRoundProjectBuyerEvaluation { get; set; }
        public AttendeeOrganizationDto BuyerAttendeeOrganizationDto { get; set; }
        public ProjectEvaluationStatus ProjectEvaluationStatus { get; set; }
        public ProjectEvaluationRefuseReason ProjectEvaluationRefuseReason { get; set; }
        public MusicBusinessRoundProjectDto MusicBusinessRoundProjectDto { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectBuyerEvaluationDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectBuyerEvaluationDto()
        {
        }
    }

}
