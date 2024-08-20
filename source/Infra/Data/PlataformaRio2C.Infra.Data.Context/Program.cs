//// ***********************************************************************
//// Assembly         : Pangea.Infra.CrossCutting.Migrations
//// Author           : Elton Assuncao
//// Created          : 02-01-2023
////
//// Last Modified By : 
//// Last Modified On : 
//// ***********************************************************************
//// <copyright file="IdentityDbContext.cs" company="Softo">
////     Copyright (c) Softo. All rights reserved.
//// </copyright>
//// <summary></summary>
//// ***********************************************************************
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Pangea.Domain.Entities;
//using Pangea.Infra.CrossCutting.Migrations.Helper;
//using Pangea.Infra.CrossCutting.MultiTenancy.Services;
//using Pangea.Infra.CrossCutting.Tools.Helpers;
//using Pangea.Infra.Data.Context.Context;

//try
//{
//    IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureAppConfiguration((config) =>
//    {     
//        var environmentName = Environment.GetEnvironmentVariable(DbHelper.Environment);
//        config.SetBasePath(Environment.CurrentDirectory)
//        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//        .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
//        .AddEnvironmentVariables()
//        .Build();
//    })

//    .ConfigureServices((context, services) =>
//    {
//        Console.WriteLine("**********************started the process****************************");

//        services.Configure<Settings>(context.Configuration.GetSection(typeof(Settings).Name));

//        var tenantConnectionStringName = context.Configuration.GetConnectionString(DbHelper.PangeaConnectionStringName);
//        Console.WriteLine($"**********************ConnectinString: {tenantConnectionStringName}**********************");
//        Console.WriteLine($"**********************ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable(DbHelper.Environment)}**********************");

//        var defaultConnectionStringName = context.Configuration.GetConnectionString(DbHelper.DefaultConnectionStringName);

//        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

//        services.AddDbContext<GlobalDbContext>(options => options.UseNpgsql(tenantConnectionStringName,
//              optionsBuilder => optionsBuilder.MigrationsAssembly(migrationsAssembly)
//        ));

//        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(defaultConnectionStringName,
//            optionsBuilder => optionsBuilder.MigrationsAssembly(migrationsAssembly)
//        ));

//        services.AddScoped(p => new TenantService(null));

//        var migration = Environment.GetEnvironmentVariable(DbHelper.Migration);
//        if (migration=="true" || migration==null)
//        {
//            Console.WriteLine("**********************started the process of creating tenants****************************");
//            services.AddAndMigrateTenantDatabases(context.Configuration);
//        }


//    })
//    .Build();

//    host.Run();
//}
//catch (Exception exp)
//{
//    Console.Error.WriteLine(exp.Message);
//}