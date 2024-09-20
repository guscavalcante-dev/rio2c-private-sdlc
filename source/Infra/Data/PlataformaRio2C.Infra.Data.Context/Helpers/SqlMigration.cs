// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Elton Assuncao
// Created          : 08-20-2024
//
// Last Modified By : 
// Last Modified On : 
// ***********************************************************************
// <copyright file="SqlMigration" company="Softo">
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
using System.Configuration;

namespace PlataformaRio2C.Infra.Data.Context.Helpers
{
    /// <summary>
    /// Class SqlMigration
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
                var assemblyDir = GetType().Assembly.GetName().Name; //PlataformaRio2C.Infra.Data.Context.
                var absolutionAssemblyDir = GetType().FullName;  //PlataformaRio2C.Infra.Data.Context.Migrations.Initial

                assemblyDir = absolutionAssemblyDir?.Replace(assemblyDir + ".", ""); //Migrations.Initial
                var array = assemblyDir?.Split('.');
                
                var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                           
                return Path.Combine(path, array[0]);
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
                    throw new Exception(string.Format("The migrations directory could not be found. '{0}'. Path not found.", MigrationsDir));

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
                                       
                    var environment = ConfigurationManager.AppSettings["Environment"];
                    if (environment == null) return true;

                    if (name == environment.ToUpper())
                    {
                        return true;
                    }
                }
            }

            return result;
        }
    }
}

