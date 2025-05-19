// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Renan Valentim
// Created          : 05-19-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-19-2025
// ***********************************************************************
// <copyright file="Seeder.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace PlataformaRio2C.Infra.Data.Context
{
    public class Seeder
    {
        public static int Seed(string connectionParam, bool autoRun = false, bool stopOnException = false)
        {
            #region Setup connection string

            // default connection
            var connectionString = ConfigurationManager.ConnectionStrings["PlataformaRio2CConnection"].ConnectionString;

            if (!string.IsNullOrEmpty(connectionParam))
            {
                Console.WriteLine("Overriding connection using connection param");
                Console.WriteLine("		New connection: " + connectionParam);
                connectionString = connectionParam;
            }
            // Check environment connection setting and overrides config, and override
            else if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PlataformaRio2CConnection")))
            {
                Console.WriteLine("Overriding connection using environment variable");
                Console.WriteLine("		New connection: " + Environment.GetEnvironmentVariable("PlataformaRio2CConnection"));
                connectionString = Environment.GetEnvironmentVariable("PlataformaRio2CConnection");
            }

            // setup connection string for Master db
            var masterConnStr = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" }.ConnectionString;

            #endregion

            try
            {
                using (var ctx = new PlataformaRio2CContext(connectionString))
                {
                    ctx.Database.CommandTimeout = 5000;
                    var targetDb = ctx.Database.Connection.Database;

                    if (!autoRun && AnswerTo("Run seed on the database \"" + targetDb + "\"? Y/n").IsNo())
                    {
                        Console.WriteLine("Seed canceled");
                        return 0;
                    }

                    #region Check if __MigrationFiles table exists

                    Console.WriteLine("Checking if __MigrationFiles table exists...");
                    if (!MigrationFilesTableExists(ctx))
                    {
                        if (!autoRun && AnswerTo("The table __MigrationFiles doesn't exists. Create? Y/n").IsNo())
                        {
                            Console.WriteLine("Seed canceled");
                            return 0;
                        }

                        Console.WriteLine("    Creating __MigrationFiles table");

                        ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                            @"CREATE TABLE [dbo].[__MigrationFiles](
	                            [Id] [int] IDENTITY(1,1) NOT NULL,
	                            [FileName] [nvarchar](200) NOT NULL,
	                            [CreatedAt] [datetime2](7) NOT NULL DEFAULT (GETDATE()))");
                    }

                    #endregion

                    Console.WriteLine("Seeder will run on " + targetDb);
                    if (!ctx.Database.Exists())
                    {
                        #region Create Database 

                        Console.WriteLine("    Creating database");

                        // connect to master and create the database
                        ctx.Database.Connection.ConnectionString = masterConnStr;
                        ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_Cp1_CI_AI;", targetDb));

                        // reconnect to Sparrow and create tables
                        var notConnected = true;
                        var i = 0;
                        while (notConnected)
                        {
                            if (i > 30)
                            {
                                throw new Exception("Database connection exceded 30 tries.");
                            }
                            ctx.Database.Connection.Close();
                            try
                            {
                                ctx.Database.Connection.ConnectionString = connectionString;
                                ctx.Database.Connection.Open();
                                notConnected = false;
                            }
                            catch (Exception e)
                            {
                                if (e.HResult != -2146232060)
                                {
                                    Console.WriteLine(e.ToString());
                                    Console.ReadKey();
                                }
                            }
                            i++;
                            Thread.Sleep(1000);
                        }

                        //ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, File.ReadAllText("Schema.sql"));
                        //ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, File.ReadAllText("CreateMigrationTable.sql"));
                        //ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, File.ReadAllText("CreateUsersView.sql"));

                        FileInfo[] sqlFiles = GetMigrationScripts();

                        Console.WriteLine("    Running migration scripts");
                        foreach (FileInfo fileinfo in sqlFiles)
                        {
                            Console.Write("       - Processing script: " + fileinfo.Name + " ...");
                            ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, File.ReadAllText(fileinfo.FullName));
                            ctx.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "INSERT INTO __MigrationFiles(FileName) VALUES({0})", fileinfo.Name);
                            Console.Write("  Done! \n");
                        }

                        #endregion
                    }
                    else
                    {
                        #region Update Database

                        ctx.Database.Connection.ConnectionString = connectionString;
                        var MigrationFiles = ctx.Database.SqlQuery<String>("SELECT FileName FROM __MigrationFiles").ToArray();

                        FileInfo[] sqlFiles = GetMigrationScripts();

                        Console.WriteLine("    Checking New Migration Scripts");

                        foreach (FileInfo fileinfo in sqlFiles)
                        {
                            if (!MigrationFiles.Contains(fileinfo.Name))
                            {
                                Console.Write("       - Processing script: " + fileinfo.Name + " ...");
                                ctx.Database.ExecuteSqlCommand(File.ReadAllText(fileinfo.FullName));
                                ctx.Database.ExecuteSqlCommand("INSERT INTO __MigrationFiles(FileName) VALUES({0})", fileinfo.Name);
                                Console.Write(" Done! \n");
                            }
                        }

                        #endregion
                    }

                    //TODO: Implement this if code-based seeds is needed.
                    //Console.WriteLine("    Seeding database");
                    //var seeder = new Seeder();
                    //seeder.Seed(ctx);
                    //ctx.SaveChanges();
                    //Console.WriteLine("    Updating Admin Roles");
                    //ctx.Database.ExecuteSqlCommand(File.ReadAllText("UpdateAdminRoles.sql"));

                    Console.WriteLine("Done");
                }

                if (!autoRun)
                {
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                Console.WriteLine();
                Console.WriteLine("Validation errors ocurred:");
                Console.WriteLine();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    var type = validationErrors.Entry.Entity.GetType();

                    Console.WriteLine("  {0} ({1}) Errors", type.Name, type.FullName);

                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        var length = validationError.PropertyName.Length;
                        Console.WriteLine("    {0}{1}", validationError.PropertyName.PadRight(length > 10 ? length + 1 : 10), validationError.ErrorMessage);
                    }
                    Console.WriteLine();
                }

                if (autoRun && !stopOnException)
                    return -1;

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Thrown:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Stack Trace:");
                Console.WriteLine(e.StackTrace);

                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.Message);
                    inner = inner.InnerException;
                }

                if (autoRun && !stopOnException)
                    return -1;

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }

            return 0;
        }

        #region Helpers

        private static string AnswerTo(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        private static string GetFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Seeder\Scripts\");
        }

        private static FileInfo[] GetMigrationScripts()
        {
            var dir = new DirectoryInfo(GetFullPath());
            FileInfo[] SqlFiles = dir.GetFiles("*.sql");

            return SqlFiles;
        }

        private static bool MigrationFilesTableExists(PlataformaRio2CContext ctx)
        {
            try
            {
                var result = ctx.Database.SqlQuery<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '__MigrationFiles'").First();
                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }

    public static class CommandLineExtensions
    {
        public static bool IsYes(this string answer, bool @default = true)
        {
            if (string.IsNullOrEmpty(answer)) return @default;

            answer = answer.Replace("\r\n\t", string.Empty).Trim().ToLower();

            return answer == "y" || answer == "yes";
        }

        public static bool IsNo(this string answer, bool @default = false)
        {
            if (string.IsNullOrEmpty(answer)) return @default;

            answer = answer.Replace("\r\n\t", string.Empty).Trim().ToLower();

            return answer == "n" || answer == "no";
        }
    }
}
