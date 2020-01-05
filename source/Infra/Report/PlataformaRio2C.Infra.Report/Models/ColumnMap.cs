// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Report
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="ColumnMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;

namespace PlataformaRio2C.Infra.Report.Models
{
    /// <summary>
    /// Classe que representa o mapeamento de uma coluna da tabela padrão criada pelo método PdfDocument.AddTable()
    /// </summary>
    public class ColumnMap
    {
        internal string ColumnHeader { get; set; }
        internal string PropertyPath { get; set; }
        internal string StringFormat { get; set; }
        internal float WidthPercentage { get; set; }
        internal int Alignment { get; set; }
        internal float FontSize { get; set; }
        internal string ColumnFooter { get; set; }

        /// <summary>
        /// Constrói um objeto de mapeamento de uma coluna para uma tabela padrão
        /// </summary>
        /// <param name="columnHeader">Texto descritivo no topo da coluna</param>
        /// <param name="propertyPath">Caminho da propriedade com o valor para a esta coluna no objeto especificado pela coleção associada à tabela</param>
        /// <param name="widthPercentage">Formato string para ser usado como argumento na sobrecarga do método ToString() do tipo</param>
        /// <param name="stringFormat">Alinhamento do texto na coluna</param>
        /// <param name="alignment">Largura percentual da coluna na tabela</param>
        /// <param name="columnFooter">Texto para o conteúdo do rodapé. Nulo em todas as colunas significa que não há rodapé.</param>
        public ColumnMap(string columnHeader, string propertyPath, float widthPercentage, string stringFormat = null, int alignment = Element.ALIGN_LEFT, string columnFooter = null)
        {
            this.ColumnHeader = columnHeader;
            this.PropertyPath = propertyPath;
            this.WidthPercentage = widthPercentage;
            this.StringFormat = stringFormat;
            this.Alignment = alignment;
            this.ColumnFooter = columnFooter;
        }
    }
}