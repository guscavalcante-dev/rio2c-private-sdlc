using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundProjectExpectationsForMeetingDto
    {
        public int MusicBusinessRoundProjectId { get; set; }
        public int LanguageId { get; set; }
        public string Value { get; set; } 

        public Language Language { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectExpectationsForMeetingDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectExpectationsForMeetingDto()
        {
        }
    }

}
