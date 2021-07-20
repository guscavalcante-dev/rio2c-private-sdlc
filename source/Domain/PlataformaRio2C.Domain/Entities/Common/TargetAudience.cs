// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="TargetAudience.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>TargetAudience</summary>
    public class TargetAudience : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;

        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }
        public int ProjectTypeId { get; private set; }

        public virtual ProjectType ProjectType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="TargetAudience"/> class.</summary>
        protected TargetAudience()
        {
        }

        public TargetAudience(string name)
        {
            SetName(name);
        }


        public void SetName(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            return true;
        }

        /// <summary>Gets the name translation.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public string GetNameTranslation(string languageCode)
        {
            return this.Name?.GetSeparatorTranslation(languageCode, Language.Separator);
        }

    }
}
