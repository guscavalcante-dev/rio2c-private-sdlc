// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 01-14-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 01-14-2019
// ***********************************************************************
// <copyright file="AudiovisualProjectSubscriptionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Dtos.Reports
{
    /// <summary>AudiovisualProjectSubscriptionDto/// </summary>
    public class AudiovisualProjectSubscriptionDto
    {
        public int ProoducerQty { get; set; }
        public int ProjectPerProducerQty { get; set; }
        public ProjectDto ProjectDto { get; set; }
    }
}
