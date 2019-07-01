// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 07-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="BreadcrumbHelper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Helpers
{
    /// <summary>BreadcrumbHelper</summary>
    public class BreadcrumbHelper
    {
        public string Title { get; private set; }
        public List<BreadcrumbItemHelper> Items { get; private set; }
        public string MenuName { get; private set; }
        public string MenuColorClass { get; private set; }
        public List<BreadcrumbMenuActionHelper> MenuActions { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="BreadcrumbHelper"/> class.</summary>
        public BreadcrumbHelper()
        {
            this.Items = new List<BreadcrumbItemHelper>();
        }

        /// <summary>Initializes a new instance of the <see cref="BreadcrumbHelper"/> class.</summary>
        /// <param name="title">The title.</param>
        /// <param name="items">The items.</param>
        public BreadcrumbHelper(string title, List<BreadcrumbItemHelper> items)
        {
            this.Title = title;
            this.Items = items;
        }

        /// <summary>Initializes a new instance of the <see cref="BreadcrumbHelper"/> class.</summary>
        /// <param name="title">The title.</param>
        /// <param name="items">The items.</param>
        /// <param name="menuName">Name of the menu.</param>
        /// <param name="menuColorClass">The menu color class.</param>
        /// <param name="menuActions">The menu actions.</param>
        public BreadcrumbHelper(string title, List<BreadcrumbItemHelper> items, string menuName, string menuColorClass, List<BreadcrumbMenuActionHelper> menuActions)
        {
            this.Title = title;
            this.Items = items;
            this.MenuName = menuName;
            this.MenuColorClass = menuColorClass;
            this.MenuActions = menuActions;
        }
    }

    /// <summary>BreadcrumbItemHelper</summary>
    public class BreadcrumbItemHelper
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="BreadcrumbItemHelper"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        public BreadcrumbItemHelper(string name, string url)
        {
            this.Name = name;
            this.Url = url;
        }
    }

    /// <summary>BreadcrumbMenuActionHelper</summary>
    public class BreadcrumbMenuActionHelper
    {
        public string Name { get; private set; }
        public string Id { get; private set; }
        public string Url { get; private set; }
        public string OnClick { get; private set; }
        public string CssClasses { get; private set; }
        public bool? IsModal { get; private set; }
        public string Icon { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="BreadcrumbMenuActionHelper"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="url">The URL.</param>
        /// <param name="onclick">The onclick.</param>
        /// <param name="cssClasses">The CSS classes.</param>
        /// <param name="isModal">The is modal.</param>
        /// <param name="icon">The icon.</param>
        public BreadcrumbMenuActionHelper(string name, string id, string url, string onclick, string cssClasses, bool? isModal, string icon)
        {
            this.Name = name;
            this.Id = id;
            this.Url = url;
            this.OnClick = onclick;
            this.CssClasses = cssClasses;
            this.IsModal = isModal;
            this.Icon = icon;
        }
    }
}