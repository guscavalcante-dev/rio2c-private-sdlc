// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-11-2019
// ***********************************************************************
// <copyright file="Role.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Domain.Constants
{
    /// <summary>Role</summary>
    public class Role
    {
        public const string Admin = "Admin | Full";
        public const string AdminPartial = "Admin | Partial";
        public const string User = "User";

        // Role groups
        public const string AnyAdmin = Role.Admin + "," + Role.AdminPartial;
        public static readonly string[] AnyAdminArray = { Role.Admin, Role.AdminPartial };
    }
}