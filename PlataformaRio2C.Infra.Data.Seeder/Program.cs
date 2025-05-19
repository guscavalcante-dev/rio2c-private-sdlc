// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Seeder
// Author           : Renan Valentim
// Created          : 05-07-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-07-2025
// ***********************************************************************
// <copyright file="Program.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.Data.Seeder
{
    public class Program
    {
        private static int Main(string[] args)
        {
            var stopOnException = false;
            var autoRun = false;
            var connectionParam = "";

            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--stop-on-exception":
                        stopOnException = true;
                        break;
                    case "--auto":
                        autoRun = true;
                        break;
                    case "--delete":
                        break;
                    case "--connection":
                        connectionParam = args[++i];
                        break;
                }
            }

            return Context.Seeder.Seed(connectionParam, autoRun, stopOnException);
        }
    }
}
