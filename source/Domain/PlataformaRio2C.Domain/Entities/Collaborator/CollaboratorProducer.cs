// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="CollaboratorProducer.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>CollaboratorProducer</summary>
    public class CollaboratorProducer : IEntity
    {
        public Guid Uid { get; set; }

        public int CollaboratorId { get; set; }
        public virtual Collaborator Collaborator { get; set; }

        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }
        public int EventId { get; set; }
        public virtual Edition Edition { get; set; }

        public virtual ValidationResult ValidationResult { get; set; }     

        public bool IsValid()
        {
            return true;
        }
    }
}
