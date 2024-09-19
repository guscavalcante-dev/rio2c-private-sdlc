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

        public int ProjectTypeId { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsCommission { get; private set; }

        public virtual ProjectType ProjectType { get; private set; }

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

        #region Audiovisual

        public static InterestGroup AudiovisualLookingFor = new InterestGroup(new Guid("D45503A8-40D9-4CD8-8DB3-D76F2F24FAE7"));
        public static InterestGroup AudiovisualProjectStatus = new InterestGroup(new Guid("379A311F-3BEC-4A9F-8E6E-539B1FD8AB87"));
        public static InterestGroup AudiovisualPlatforms = new InterestGroup(new Guid("6590E0F1-B8DA-45D0-BE2C-E4B7CCF3751B"));
        public static InterestGroup AudiovisualGenre = new InterestGroup(new Guid("7B4A7C4A-EF10-483C-8854-87EBEB883583"));
        public static InterestGroup AudiovisualSubGenre = new InterestGroup(new Guid("BBFA501D-A4D2-4500-8D7D-8A133685E6D2"));
        public static InterestGroup AudiovisualFormat = new InterestGroup(new Guid("2D5AE955-8D8F-4763-AEE4-964980FFB170"));

        #endregion

        #region Creator

        public static InterestGroup CreatorFormat = new InterestGroup(new Guid("FAA61D07-06BB-4B4F-A01C-2FEA91262C37"));
        public static InterestGroup CreatorSegment = new InterestGroup(new Guid("8F1715A1-CE19-4F44-9BC7-A797D620CE71"));
        public static InterestGroup CreatorSubGenre = new InterestGroup(new Guid("66FCBCD9-D915-4DAE-AFF7-4BF8C674A1E9"));

        #endregion

        #region Music

        public static InterestGroup MusicLookingFor = new InterestGroup(new Guid("33AE337F-99F1-4C8D-98EC-8044572A104D"));

        #endregion

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
