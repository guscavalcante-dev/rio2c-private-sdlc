// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-16-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="CollaboratorApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorApiDto</summary>
    public class CollaboratorApiDto : CollaboratorApiListDto
    {
        public string Website { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorApiDto"/> class.</summary>
        public CollaboratorApiDto()
        {
        }
    }
}