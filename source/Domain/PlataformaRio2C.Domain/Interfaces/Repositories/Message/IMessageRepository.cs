// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="IMessageRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IMessageRepository</summary>
    public interface IMessageRepository : IRepository<Message>
    {
        Task<ConversationDto> FindNewConversationsDtoByEditionIdAndByOtherUserUid(int editionId, Guid otherUserUid);
        Task<List<ConversationDto>> FindAllConversationsDtosByEditionIdAndByUserId(int editionId, int userId);
        Task<List<MessageDto>> FindAllMessagesDtosByEditionIdAndByUserIdAndByRecipientIdAndByRecipientUid(int editionId, int userId, int recipientId, Guid recipientUid);
    }    
}
