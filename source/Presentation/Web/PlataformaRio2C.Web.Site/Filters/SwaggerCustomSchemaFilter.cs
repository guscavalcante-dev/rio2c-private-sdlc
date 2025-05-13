// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 10-23-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="SwaggerCustomSchemaFilter.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using Swashbuckle.Swagger;
using System;
using System.Linq;
using System.Reflection;

namespace PlataformaRio2C.Web.Site.Filters
{
    /// <summary>
    /// SwaggerCustomSchemaFilter
    /// </summary>
    /// <seealso cref="Swashbuckle.Swagger.ISchemaFilter" />
    public class SwaggerCustomSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            foreach (var property in type.GetProperties())
            {
                var parameterAttribute = property.GetCustomAttributes<SwaggerParameterDescriptionAttribute>().FirstOrDefault();
                if (parameterAttribute != null)
                {
                    var schemaProperty = schema.properties[property.Name.LowercaseFirst()];
                    if (schemaProperty != null)
                    {
                        schemaProperty.description = parameterAttribute.GetDescription();
                    }
                }
            }
        }
    }
}