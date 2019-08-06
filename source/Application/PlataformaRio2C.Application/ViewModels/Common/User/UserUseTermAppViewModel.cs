// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="UserUseTermAppViewModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.ViewModels
{
    /// <summary>UserUseTermAppViewModel</summary>
    public class UserUseTermAppViewModel : EntityViewModel<UserUseTermAppViewModel, UserUseTerm>, IEntityViewModel<UserUseTerm>
    {
        public int UserId { get; set; }
        public int EventId { get; set; }

        public string Role { get; set; }

        public UserUseTermAppViewModel()
        {

        }

        public UserUseTermAppViewModel(UserUseTerm entity)
        {
            Uid = entity.Uid;
            CreationDate = entity.CreateDate;

            if (entity.User != null)
            {
                UserId = entity.User.Id;
            }

            if (entity.Edition != null)
            {
                EventId = entity.Edition.Id;
            }           

            if (entity.Role != null)
            {
                Role = entity.Role.Name;
            }
        }              

        public UserUseTerm MapReverse()
        {
            var entity = new UserUseTerm(UserId, EventId);

            return entity;
        }

        public UserUseTerm MapReverse(UserUseTerm entity)
        {
            entity.SetUserId(UserId);

            entity.SetEventId(EventId);

            return entity;
        }
    }
}
