// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-21-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="DeleteProject.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>
    /// DeleteProject
    /// </summary>
    public class DeleteProject : BaseCommand
    {
        public Guid? ProjectUid { get; set; }

        public DeleteProject(ProjectDto entity)
        {
            this.ProjectUid = entity?.Project?.Uid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProject"/> class.
        /// </summary>
        public DeleteProject()
        {
        }
    }
}