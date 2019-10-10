// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
// ***********************************************************************
// <copyright file="ValidationError.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Enums;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Validation
{
    /// <summary>ValidationError</summary>
    public class ValidationError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> MemberNames { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
        /// <param name="message">The message.</param>
        public ValidationError(string message)
        {
            Message = message;
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public ValidationError(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="memberNames">The member names.</param>
        public ValidationError(string message, IEnumerable<string> memberNames)
        {
            Message = message;
            MemberNames = memberNames;
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationError"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="memberNames">The member names.</param>
        /// <param name="erroCode">The erro code.</param>
        public ValidationError(string message, IEnumerable<string> memberNames, ErrorCodes erroCode)
        {
            Message = message;
            MemberNames = memberNames;
            Code = erroCode.ToString();
        }
    }
}
