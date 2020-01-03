// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-27-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="ColumnMap" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using iTextSharp.text;

namespace PlataformaRio2C.Infra.Report
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
        /// <param name="ColumnHeader">Texto descritivo no topo da coluna</param>
        /// <param name="PropertyPath">Caminho da propriedade com o valor para a esta coluna no objeto especificado pela coleção associada à tabela</param>
        /// <param name="StringFormat">Formato string para ser usado como argumento na sobrecarga do método ToString() do tipo</param>
        /// <param name="Alignment">Alinhamento do texto na coluna</param>
        /// <param name="WidthPercentage">Largura percentual da coluna na tabela</param>
        /// <param name="ColumnFooter">Texto para o conteúdo do rodapé. Nulo em todas as colunas significa que não há rodapé.</param>
        public ColumnMap(string ColumnHeader, string PropertyPath, float WidthPercentage, string StringFormat = null, int Alignment = Element.ALIGN_LEFT, string ColumnFooter = null)
        {
            this.PropertyPath = PropertyPath;
            this.ColumnHeader = ColumnHeader;
            this.StringFormat = StringFormat;
            this.WidthPercentage = WidthPercentage;
            this.Alignment = Alignment;
            this.ColumnFooter = ColumnFooter;
        }
    }
}
