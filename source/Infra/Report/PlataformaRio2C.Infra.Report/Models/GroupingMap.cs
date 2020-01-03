// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="GroupingMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PlataformaRio2C.Infra.Report
{
    /// <summary>
    /// Mapeia o agrupamento em primeiro nível da coleção especificada no método SETDocument.AddTable()
    /// </summary>
    public class GroupingMap
    {
        /// <summary>
        /// Propriedade de agrupamento na coleção especificada
        /// </summary>
        public string GroupPropertyPath { get; private set; }

        /// <summary>
        /// Especifica se os subtotais devem somar os elementos do grupo ou ser cumulativos em relação à coleção
        /// </summary>
        public bool GlobalSubtotals { get; private set; }

        /// <summary>
        /// Índice das colunas que irão exibir os somatórios
        /// </summary>
        public uint[] SubtotalColumnIndexes { get; private set; }


        internal Dictionary<uint, ColumnSubtotal> SubTotals { get; private set; }

        /// <summary>
        /// Constrói um objeto utilizado pelo método SETDocument.AddTable() para mapear um agrupamento da coleção especificada
        /// </summary>
        /// <param name="groupPropertyPath">Propriedade de agrupamento na coleção</param>
        /// <param name="subtotalColumnIndexes">Índices das colunas cujo somatório deve ser realizado</param>
        /// <param name="globalSubtotals">Especifica se o subtotal irá considerar o grupo imediato somente ou incluir o somatório de grupos anteriores</param>
        public GroupingMap(string groupPropertyPath, uint[] subtotalColumnIndexes, bool globalSubtotals = false)
        {
            if (string.IsNullOrEmpty(groupPropertyPath))
                throw new ArgumentException("O nome da propriedade de agrupamento não pode ser nulo ou vazio", nameof(groupPropertyPath));

            if (subtotalColumnIndexes == null || subtotalColumnIndexes.Length == 0)
                throw new ArgumentException("O conjunto de índices das colunas de somatório não pode ser nulo ou vazio", nameof(subtotalColumnIndexes));

            GroupPropertyPath = groupPropertyPath;
            SubtotalColumnIndexes = subtotalColumnIndexes;
            GlobalSubtotals = globalSubtotals;

            SubTotals = new Dictionary<uint, ColumnSubtotal>();
            for (uint i = 0; i < SubtotalColumnIndexes.Length; i++)
                SubTotals.Add(SubtotalColumnIndexes[i], new ColumnSubtotal() { ColumnIndex = i });
        }

        internal void ResetSubTotals()
        {
            foreach (var subtotal in SubTotals)
                subtotal.Value.Subtotal = 0;
        }
    }

    internal class ColumnSubtotal
    {
        public uint ColumnIndex { get; set; }

        public ColumnMap ColumnMap { get; set; }

        public PropertyInfo ColumnProperty { get; set; }

        public decimal Subtotal { get; set; }
    }

    internal class GroupedTableEvent : IPdfPTableEvent
    {
        public void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases)
        {
            throw new NotImplementedException();
        }
    }
}
