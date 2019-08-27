// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-27-2019
// ***********************************************************************
// <copyright file="CollaboratorBaseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorBaseDto</summary>
    public class CollaboratorBaseDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        public string FullName => this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);
        public string FirstName { get; set; }
        public string LastNames { get; set; }
        public string Badge { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public HoldingBaseDto HoldingBaseDto { get; set; }
        public OrganizationBaseDto OrganizatioBaseDto { get; set; }
        public DateTime? ImageUploadDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsInCurrentEdition { get; set; }
        public bool IsInOtherEdition { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CollaboratorBaseDto"/> class.</summary>
        public CollaboratorBaseDto()
        {
        }
    }
}