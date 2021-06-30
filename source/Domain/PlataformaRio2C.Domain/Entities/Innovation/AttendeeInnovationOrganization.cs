// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganization.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganization : Entity
    {
        public int EditionId { get; set; }
        public int InnovationOrganizationId { get; set; }
        public int ProjectEvaluationStatusId { get; set; }
        public int? ProjectEvaluationRefuseReasonId { get; set; }
        public string Reason { get; set; }
        public int? EvaluationUserId { get; set; }
        public DateTimeOffset? EvaluationEmailSendDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganization"/> class.
        /// </summary>
        public AttendeeInnovationOrganization()
        {

        }

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
