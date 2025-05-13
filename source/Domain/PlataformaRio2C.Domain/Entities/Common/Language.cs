// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-24-2020
// ***********************************************************************
// <copyright file="Language.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Language</summary>
    public class Language : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;

        #region Configurations

        public static Language Portuguese = new Language("pt-br");
        public static Language English = new Language("en-us");

        public static List<string> CodesOrder = new List<string> { Portuguese.Code, English.Code };
        public static char Separator = '|';

        #endregion

        public string Name { get; private set; }
        public string Code { get; private set; }
        public bool IsDefault { get; private set; }
        public bool IsActive { get; private set; }

        public virtual ICollection<User> Users { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Language"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        public Language(string name, string code)
        {
            this.Name = name?.Trim();
            this.Code = code?.Trim();
        }

        /// <summary>Initializes a new instance of the <see cref="Language"/> class.</summary>
        protected Language()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Language"/> class.</summary>
        /// <param name="code">The code.</param>
        private Language(string code)
        {
            this.Code = code?.Trim();
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}