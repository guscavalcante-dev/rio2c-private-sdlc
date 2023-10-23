// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 08-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-28-2019
// ***********************************************************************
// <copyright file="SwaggerCustomParameterFilter.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace PlataformaRio2C.Web.Site.Filters
{
    public class SwaggerCustomParameterFilter : IOperationFilter
    {
        //public void Apply(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    IEnumerable<SwaggerParameterExampleAttribute> parameterAttributes = null;

        //    if (context.PropertyInfo != null)
        //    {
        //        parameterAttributes = context.PropertyInfo.GetCustomAttributes<SwaggerParameterExampleAttribute>();
        //    }
        //    else if (context.ParameterInfo != null)
        //    {
        //        parameterAttributes = context.ParameterInfo.GetCustomAttributes<SwaggerParameterExampleAttribute>();
        //    }

        //    if (parameterAttributes != null && parameterAttributes.Any())
        //    {
        //        AddExample(parameter, parameterAttributes);
        //    }
        //}

        //public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        //{
        //    throw new System.NotImplementedException();
        //}

        //private void AddExample(OpenApiParameter parameter, IEnumerable<SwaggerParameterExampleAttribute> parameterAttributes)
        //{
        //    foreach (var item in parameterAttributes)
        //    {
        //        var example = new OpenApiExample
        //        {
        //            Value = new Microsoft.OpenApi.Any.OpenApiString(item.Value)
        //        };

        //        parameter.Examples.Add(item.Name, example);
        //    }
        //}

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            throw new System.NotImplementedException();
        }
    }
}