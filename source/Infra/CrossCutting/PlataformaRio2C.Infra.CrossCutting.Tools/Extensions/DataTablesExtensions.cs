// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 08-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-10-2019
// ***********************************************************************
// <copyright file="DataTablesExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using DataTables.AspNet.Core;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>DataTablesExtensions</summary>
    public static class DataTablesExtensions
    {
        /// <summary>Gets the sort columns.</summary>
        /// <param name="dataTablesRequest">The data tables request.</param>
        /// <returns></returns>
        public static List<Tuple<string, string>> GetSortColumns(this IDataTablesRequest dataTablesRequest)
        {
            return dataTablesRequest?.Columns?.Where(c => c.Sort != null)?.OrderBy(c => c.Sort.Order)?.Select(c => new Tuple<string, string>(c.Field, c.Sort.Direction.ToString()))?.ToList();
        }

        /// <summary>Dynamics the order.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="allowedColumns">The allowed columns.</param>
        /// <param name="defaultSortColumn">The default sort column.</param>
        /// <returns></returns>
        public static IQueryable<T> DynamicOrder<T>(this IQueryable<T> query, List<Tuple<string, string>> sortColumns, List<string> allowedColumns, string defaultSortColumn)
        {
            var hasSortColumn = false;

            var orderBy = string.Empty;

            if (allowedColumns?.Any() == true)
            {
                var allowedSortColumns = sortColumns?.Where(sc => allowedColumns.Select(ac => ac.ToLowerInvariant()).Contains(sc.Item1.ToLowerInvariant())).ToList();

                if (allowedSortColumns?.Any() == true)
                {
                    foreach (var sortColumn in allowedSortColumns)
                    {
                        orderBy += (!string.IsNullOrEmpty(orderBy) ? ", " : string.Empty) + sortColumn.Item1 + (sortColumn.Item2 == "Descending" ? " desc" : string.Empty);
                        hasSortColumn = true;
                    }

                    query = query.OrderBy(orderBy);
                }
            }

            if (!hasSortColumn)
            {
                query = query.OrderBy(defaultSortColumn);
            }

            return query;
        }
    }
}