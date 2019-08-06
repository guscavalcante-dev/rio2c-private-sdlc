// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="QuizDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.Dtos
{
    /// <summary>QuizDto</summary>
    public class QuizDto
    {
        public Guid Uid { get; private set; }
        public int EditionId { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreateDate { get; private set; }
        public int CreateUserId { get; private set; }
        public int UpdateUserId { get; private set; }
        public DateTime UpdateDate { get; private set; }

        //public UserAppViewModel Creator { get; set; }
        //public UserAppViewModel Updated { get; set; }

        /// <summary>Initializes a new instance of the <see cref="QuizDto"/> class.</summary>
        public QuizDto()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="QuizDto"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public QuizDto(Domain.Entities.Quiz entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Uid = entity.Uid;
            this.EditionId = entity.EditionId;
            this.Name = entity.Name;
            this.IsActive = entity.IsActive;
            this.CreateDate = entity.CreateDate;
            this.CreateUserId = entity.CreateUserId;
            this.UpdateDate = entity.UpdateDate;
            this.UpdateUserId = entity.UpdateUserId;

            //if (entity.Creator != null)
            //{
            //    this.Creator = new UserAppViewModel(entity.Creator);
            //}

            //if (entity.Updater != null)
            //{
            //    this.Updater = new UserAppViewModel(entity.Updater);
            //}
        }
    }
}
