// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="InnovationOptionGroup.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class InnovationOrganization.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class InnovationOptionGroup : Entity
    {
        #region Configurations

        public static InnovationOptionGroup CompanyExperiences = new InnovationOptionGroup(new Guid("752598D8-D445-48F7-BE41-6A1B110FA749"), "Quais dessas experiências a empresa já participou?", 1);
        public static InnovationOptionGroup ProductsOrServicesTracks = new InnovationOptionGroup(new Guid("50A702A9-D189-4F65-BEF9-88365D73BA66"), "Enquadre seu produto ou serviço num track abaixo:", 2);
        public static InnovationOptionGroup TechnologyExperiences = new InnovationOptionGroup(new Guid("AB308844-5B2F-41E1-9A11-C00088AD2DDF"), "Tecnologia usadas:", 3);
        public static InnovationOptionGroup CompanyObjectives = new InnovationOptionGroup(new Guid("2ACBCB51-8520-4767-A5C8-927B8CC007E5"), "Qual o seu principal objetivo em participar das Pitching de Startups?", 4);

        #endregion

        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        
        public virtual ICollection<InnovationOption> InnovationOptions { get; private set; }

        public InnovationOptionGroup(Guid innovationOptionUid, string name, int displayOrder)
        {
            this.Uid = innovationOptionUid;
            this.Name = name;
            this.DisplayOrder = displayOrder;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOptionGroup"/> class.
        /// </summary>
        public InnovationOptionGroup()
        {

        }

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
