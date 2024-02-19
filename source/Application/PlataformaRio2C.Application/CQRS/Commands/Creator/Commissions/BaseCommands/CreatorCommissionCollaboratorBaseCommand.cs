// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-08-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-06-2023
// ***********************************************************************
// <copyright file="CreatorCommissionCollaboratorBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    public class CreatorCommissionCollaboratorBaseCommand : CollaboratorBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatorCommissionCollaboratorBaseCommand" /> class.
        /// </summary>
        public CreatorCommissionCollaboratorBaseCommand()
        {
        }
    }
}