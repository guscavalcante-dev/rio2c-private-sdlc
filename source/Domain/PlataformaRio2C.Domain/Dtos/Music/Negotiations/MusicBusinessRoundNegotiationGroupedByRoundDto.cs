using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundNegotiationGroupedByRoundDto
    {
        public int RoundNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<MusicBusinessRoundNegotiation> MusicBusinessRoundNegotiations { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByRoundDto"/> class.</summary>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="negotiations">The negotiations.</param>
        public MusicBusinessRoundNegotiationGroupedByRoundDto(int roundNumber, List<MusicBusinessRoundNegotiation> negotiations)
        {
            this.RoundNumber = roundNumber;
            this.StartDate = negotiations?.FirstOrDefault()?.StartDate.ToBrazilTimeZone();
            this.EndDate = negotiations?.FirstOrDefault()?.EndDate.ToBrazilTimeZone();
            this.MusicBusinessRoundNegotiations = negotiations?.OrderBy(n => n.TableNumber)?.ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByRoundDto"/> class.</summary>
        public MusicBusinessRoundNegotiationGroupedByRoundDto()
        {
        }
    }
}
