// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Elton Assunção
// Created          : 01-04-2024
//
// Last Modified By : Elton Assunção
// Last Modified On :01-04-2024
// ***********************************************************************
// <copyright file="SwaggerCustomDefaulValueFilter.cs" company="Softo">
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
    /// SwaggerCustomDefaulValueFilter
    /// </summary>
    /// <seealso cref="Swashbuckle.Swagger.ISchemaFilter" />
    public class SwaggerCustomDefaulValueFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="type"></param>
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            foreach (var property in type.GetProperties())
            {
                var parameterAttribute = property.GetCustomAttributes<SwaggerDefaultValueAttribute>().FirstOrDefault();
                if (parameterAttribute != null)
                {
                    var schemaProperty = schema.properties[property.Name.LowercaseFirst()];
                    if (schemaProperty != null)
                    {
                        schemaProperty.@default = parameterAttribute.Value;
                    }
                }
            }
        }
    }
}