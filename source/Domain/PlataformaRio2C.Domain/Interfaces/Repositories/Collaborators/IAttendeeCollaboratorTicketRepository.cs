// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 10-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-03-2019
// ***********************************************************************
// <copyright file="IAttendeeCollaboratorTicketRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeCollaboratorTicketRepository</summary>
    public interface IAttendeeCollaboratorTicketRepository : IRepository<AttendeeCollaboratorTicket>
    {
        Task<AttendeeCollaboratorTicketDto> FindDtoByBarcode(int editionId, string barcode);
    }
}