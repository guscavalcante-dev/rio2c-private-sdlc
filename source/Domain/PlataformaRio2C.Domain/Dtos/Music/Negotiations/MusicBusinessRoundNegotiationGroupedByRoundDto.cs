using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundNegotiationGroupedByRoundDto
    {
        public int RoundNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<MusicBusinessRoundNegotiation> Negotiations { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByRoundDto"/> class.</summary>
        /// <param name="roundNumber">The round number.</param>
        /// <param name="negotiations">The negotiations.</param>
        public MusicBusinessRoundNegotiationGroupedByRoundDto(int roundNumber, List<MusicBusinessRoundNegotiation> negotiations)
        {
            this.RoundNumber = roundNumber;
            this.StartDate = negotiations?.FirstOrDefault()?.StartDate.ToBrazilTimeZone();
            this.EndDate = negotiations?.FirstOrDefault()?.EndDate.ToBrazilTimeZone();
            this.Negotiations = negotiations?.OrderBy(n => n.TableNumber)?.ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByRoundDto"/> class.</summary>
        public MusicBusinessRoundNegotiationGroupedByRoundDto()
        {
        }
    }
}
