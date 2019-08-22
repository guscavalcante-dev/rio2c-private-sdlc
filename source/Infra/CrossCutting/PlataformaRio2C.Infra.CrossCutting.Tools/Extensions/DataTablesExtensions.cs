// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 08-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-22-2019
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
        /// <param name="changeNameColumns">The change name columns.</param>
        /// <param name="allowedColumns">The allowed columns.</param>
        /// <param name="defaultSortColumn">The default sort column.</param>
        /// <returns></returns>
        public static IQueryable<T> DynamicOrder<T>(
            this IQueryable<T> query, 
            List<Tuple<string, string>> sortColumns, 
            List<Tuple<string, string>> changeNameColumns, 
            List<string> allowedColumns, 
            string defaultSortColumn)
        {
            var hasSortColumn = false;

            var orderBy = string.Empty;

            if (allowedColumns?.Any() == true)
            {

                var finalSortColumns = GetFinalSortColumns(sortColumns, changeNameColumns, allowedColumns);
                if (finalSortColumns?.Any() == true)
                {
                    foreach (var sortColumn in finalSortColumns)
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

        /// <summary>Gets the final sort columns.</summary>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="changeNameColumns">The change name columns.</param>
        /// <param name="allowedColumns">The allowed columns.</param>
        /// <returns></returns>
        private static List<Tuple<string, string>> GetFinalSortColumns(List<Tuple<string, string>> sortColumns, List<Tuple<string, string>> changeNameColumns, List<string> allowedColumns)
        {
            var finalSortColumns = new List<Tuple<string, string>>();

            if (changeNameColumns?.Any() == true)
            {
                foreach (var sortColumn in sortColumns)
                {
                    var newColumnName = changeNameColumns.FirstOrDefault(cnc => cnc.Item1.ToLowerInvariant() == sortColumn.Item1.ToLowerInvariant());
                    finalSortColumns.Add(newColumnName != null ? new Tuple<string, string>(newColumnName.Item2, sortColumn.Item2) :
                                                                 new Tuple<string, string>(sortColumn.Item1, sortColumn.Item2));
                }
            }
            else
            {
                finalSortColumns = sortColumns;
            }

            return finalSortColumns?.Where(sc => allowedColumns.Select(ac => ac.ToLowerInvariant()).Contains(sc.Item1.ToLowerInvariant())).ToList();
        }
    }
}