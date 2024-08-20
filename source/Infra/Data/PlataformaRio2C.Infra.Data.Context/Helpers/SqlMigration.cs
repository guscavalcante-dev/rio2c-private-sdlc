// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Elton Assuncao
// Created          : 08-20-2024
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="EnvironmentVariableAttribute" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Reflection;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;


namespace PlataformaRio2C.Infra.Data.Context.Helpers
{
    /// <summary>
    /// Class ScriptMigration
    /// </summary>
    public abstract class SqlMigration : DbMigration
    {
        /// <summary>
        /// Gets the migrations dir.
        /// </summary>
        /// <value>The migrations dir.</value>
        public string MigrationsDir
        {
            get
            {
                var assemblyDir = GetType().Assembly.GetName().Name;
                var absolutionAssemblyDir = GetType().FullName;

                assemblyDir = absolutionAssemblyDir?.Replace(assemblyDir + ".", "");
                var array = assemblyDir?.Split('.');
                //Pangea.Infra.CrossCutting.Migrations.Migrations.Tenant.insert_data_types_and_system_configuration
                // "Pangea.Infra.CrossCutting.Migrations"

                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //return Path.Combine(path, "Migrations");
                return Path.Combine(path, array[0], array[1]);
            }
        }
        /// <summary>
        /// The _migration name
        /// </summary>
        private string _migrationName;

        /// <summary>
        /// Gets the name of the migration.
        /// </summary>
        /// <value>The name of the migration.</value>
        /// <exception cref="System.Exception">
        /// </exception>        
        public string MigrationName
        {
            get
            {
                if (!string.IsNullOrEmpty(_migrationName))
                    return _migrationName;
                if (!Directory.Exists(MigrationsDir))
                    throw new Exception(string.Format("The migrations directory could not be found. {0}. This path is based on DevEnv.ProjectDirectories.Infra.", MigrationsDir));

                var files = Directory.GetFiles(MigrationsDir, "*.sql", SearchOption.AllDirectories);

                var className = GetType().Name;
                foreach (var file in files)
                {

                    var fileresult = file.Replace(MigrationsDir, "").Replace(".up.sql", "").Replace(".down.sql", "");

                    if (fileresult.EndsWith(className))
                    {
                        return fileresult;
                    }
                }

                throw new Exception(string.Format(
                    "Couldn't find the file for the migration {0}. " +
                    "The file name of a migration MUST follow the format [timestamp]_[classname].sql. " +
                    "For this migration the correct file name is {1}",
                    GetType().Name,
                    "999999999999999_" + GetType().Name + ".sql"));
            }
        }

        /// <summary>
        /// Gets up script path.
        /// </summary>
        /// <value>Up script path.</value>
        public string UpScriptPath
        {
            get
            {
                return $"{MigrationsDir}{MigrationName}.up.sql";
            }
        }
        /// <summary>
        /// Gets down script path.
        /// </summary>
        /// <value>Down script path.</value>
        public string DownScriptPath
        {
            get
            {
                return $"{MigrationsDir}{MigrationName}.down.sql";
            }
        }

        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            if (ExecuteEnvironmentVariable())
            {
                var text = File.ReadAllText(UpScriptPath);
                base.Sql(text);
            }
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            if (ExecuteEnvironmentVariable())
            {
                base.Sql(File.ReadAllText(DownScriptPath));
            }
            base.Down();
        }

       
        /// <summary>
        ///  check Annotation EnvironmentVariable
        /// </summary>
        private bool ExecuteEnvironmentVariable()
        {
            bool result = true;

            var dnAttribute = GetType().GetCustomAttributes(typeof(EnvironmentVariableAttribute), true).FirstOrDefault() as EnvironmentVariableAttribute;

            if (dnAttribute != null)
            {
                result = false;

                var environmentVariables = dnAttribute.Name;
                for (int i = 0; i < environmentVariables.Length; i++)
                {
                    var name = environmentVariables[i].ToString().ToUpper();

                    if (name == "ALL") return true;

                    //todo - Verificar como esta configurado no aws. Precia criar uma variável de ambiente com dos nomes que estão nesse enum: PlataformaRio2C.Infra.CrossCutting.Tools.EnumsEnumEnvironments
                    var environment = Environment.GetEnvironmentVariable("ENVIRONMENT")?.ToUpper();
                    if (environment == null) return true;

                    if (name == environment)
                    {
                        return true;
                    }
                }
            }

            return result;
        }
    }
}

