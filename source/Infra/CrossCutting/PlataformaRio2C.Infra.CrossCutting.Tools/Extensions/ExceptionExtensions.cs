// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 07-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-19-2019
// ***********************************************************************
// <copyright file="ExceptionExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>ExceptionExtensions</summary>
    public static class ExceptionExtensions
    {
        /// <summary>Gets the inner message.</summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static string GetInnerMessage(this Exception exception)
        {
            var lastException = exception;
            while (lastException.InnerException != null)
            {
                lastException = lastException.InnerException;
            }

            return lastException.Message;
        }
    }
}
