// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Almado
// Created          : 10-09-2019
//
// Last Modified By : William Almado
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="UpdateCollaboratorInterfaceLanguage.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateCollaboratorInterfaceLanguage</summary>
    public class UpdateCollaboratorInterfaceLanguage : CollaboratorBaseCommand
    {
        public Guid CollaboratorUid { get; set; }
        public string LanguageCode { get; set; }

        public UpdateCollaboratorInterfaceLanguage(Guid collaboratorUid, string languageCode)
        {
            this.CollaboratorUid = collaboratorUid;
            this.LanguageCode = languageCode;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorInterfaceLanguage"/> class.</summary>
        public UpdateCollaboratorInterfaceLanguage(){}
    }
}