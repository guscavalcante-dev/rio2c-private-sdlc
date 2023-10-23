// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-17-2021
// ***********************************************************************
// <copyright file="InterestGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>InterestGroup</summary>
    public class InterestGroup : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 150;
        public static readonly int TypeMaxLength = 100;

        public string Name { get; private set; }
        public string Type { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsCommission { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="InterestGroup"/> class.</summary>
        protected InterestGroup()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InterestGroup"/> class.</summary>
        /// <param name="uid">The uid.</param>
        private InterestGroup(Guid uid)
        {
            this.Uid = uid;
        }

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name.GetSeparatorTranslation(languageCode, Language.Separator);
        }

        #region Statics

        public static InterestGroup LookingFor = new InterestGroup(new Guid("D45503A8-40D9-4CD8-8DB3-D76F2F24FAE7"));
        public static InterestGroup ProjectStatus = new InterestGroup(new Guid("379A311F-3BEC-4A9F-8E6E-539B1FD8AB87"));
        public static InterestGroup Platforms = new InterestGroup(new Guid("6590E0F1-B8DA-45D0-BE2C-E4B7CCF3751B"));
        public static InterestGroup Genre = new InterestGroup(new Guid("7B4A7C4A-EF10-483C-8854-87EBEB883583"));
        public static InterestGroup SubGenre = new InterestGroup(new Guid("BBFA501D-A4D2-4500-8D7D-8A133685E6D2"));
        public static InterestGroup Format = new InterestGroup(new Guid("2D5AE955-8D8F-4763-AEE4-964980FFB170"));


        
        #endregion

        #region Old methods

        public InterestGroup(string name, InterestGroupTypeCodes value)
        {
            SetName(name);
            SetType(value);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetType(InterestGroupTypeCodes value)
        {
            Type = value.ToString();
        }

        public override bool IsValid()
        {
            return true;
        }

        #endregion
    }
}
