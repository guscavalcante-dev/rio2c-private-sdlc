using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ILogisticTransferRepository : IRepository<LogisticTransfer>
    {
        Task<List<LogisticTransferDto>> FindAllDtosPaged(Guid logisticsUid);
    }    
}
