using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ILogisticsRepository : IRepository<Logistics>
    {
        Task<IPagedList<LogisticRequestBaseDto>> FindAllByDataTable(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns, bool showAllParticipants, bool showAllSponsored);
        Task<LogisticRequestBaseDto> GetDto(Guid id, Language language);
    }    
}
