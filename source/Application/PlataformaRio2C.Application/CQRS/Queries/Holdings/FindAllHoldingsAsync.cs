// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="FindAllHoldingsAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using MediatR;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllHoldingsAsync</summary>
    public class FindAllHoldingsAsync : IRequest<IPagedList<HoldingListDto>>
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public string Keywords { get; private set; }
        public List<Tuple<string, string>> SortColumns { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="FindAllHoldingsAsync"/> class.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        public FindAllHoldingsAsync(int page, int pageSize, string keywords, List<Tuple<string, string>> sortColumns)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.Keywords = keywords;
            this.SortColumns = sortColumns;
        }
    }
}