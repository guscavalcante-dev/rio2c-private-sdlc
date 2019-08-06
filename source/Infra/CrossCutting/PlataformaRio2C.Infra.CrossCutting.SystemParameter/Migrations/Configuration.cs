// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SystemParameter
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="Configuration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Context;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter.Migrations
{
    /// <summary>Configuration</summary>
    internal sealed class Configuration : DbMigrationsConfiguration<PlataformaRio2CContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlataformaRio2CContext context)
        {
            SeedSystemParameters(context);
        }

        private void SeedSystemParameters(PlataformaRio2CContext context)
        {
            var parameterDtos = default(SystemParameterCodes).SystemParametersDescriptions();

            foreach (var dto in parameterDtos)
            {
                var parameter = new SystemParameter(dto);
                var parameterExisting = context.SystemParameters.FirstOrDefault(e => e.Code == parameter.Code && e.LanguageCode == parameter.LanguageCode);
                if (parameterExisting == null)
                {
                    context.SystemParameters.Add(parameter);
                }
                else
                {
                    parameterExisting.UpdateFromDto(dto);
                }
            }
        }

        private void DeleteSystemParametersbyEnum(PlataformaRio2CContext context)
        {
            var paramesters = context.SystemParameters.ToList();
            var parametersDto = default(SystemParameterCodes).SystemParametersDescriptions();

            foreach (var parameter in paramesters)
            {
                if (!parametersDto.Any(e => parametersDto.Any(d => d.Code == e.Code)))
                {
                    context.SystemParameters.Remove(parameter);                    
                }
            }
        }
    }
}
