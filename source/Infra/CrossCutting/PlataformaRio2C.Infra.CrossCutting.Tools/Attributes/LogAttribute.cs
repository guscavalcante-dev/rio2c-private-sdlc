using log4net;
//using PostSharp.Aspects;
//using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    //[MulticastAttributeUsage(MulticastTargets.InstanceConstructor | MulticastTargets.StaticConstructor | MulticastTargets.Method, AllowMultiple = true)]
    [Serializable]
    public sealed class LogAttribute : System.Attribute// : OnMethodBoundaryAspect
    {
        private static readonly ILog Logger = LogManager.GetLogger("root");
        private static void ConfigureLogger()
        {
            if (!Logger.Logger.Repository.Configured)
            {
                log4net.GlobalContext.Properties["MachineName"] = Environment.MachineName;

                log4net.Config.XmlConfigurator.Configure();
            }
        }

        public bool LogOnExit { get; set; }

        //public override void OnEntry(MethodExecutionArgs args)
        //{
        //    if (NoLog(args)) return;

        //    ConfigureLogger();

        //    var message = ((this.LogOnExit ? "Entering: " : "") + BuildMethodMessage(args));

        //    Logger.Debug(message);
        //}

        //public override void OnExit(MethodExecutionArgs args)
        //{
        //    if (NoLog(args)) return;

        //    if (this.LogOnExit)
        //    {
        //        ConfigureLogger();

        //        var message = ("Leaving: " + BuildMethodMessage(args));

        //        Logger.Debug(message);
        //    }
        //}

        //public override void OnException(MethodExecutionArgs args)
        //{
        //    if (NoLog(args)) return;

        //    ConfigureLogger();

        //    var message = ("Exception: " + args.Exception.GetType().Name + ": " + args.Exception.Message + args.Exception.StackTrace);

        //    Logger.Debug(message);
        //}

        //private static bool NoLog(MethodExecutionArgs args)
        //{
        //    var logConfigAttribute = GetLogConfigAttribute(args);

        //    return logConfigAttribute != null && logConfigAttribute.NoLog;
        //}

        private static bool NoLog(ParameterInfo parameterInfo)
        {
            var logConfigAttribute = GetLogConfigAttribute(parameterInfo);

            return logConfigAttribute != null && logConfigAttribute.NoLog;
        }

        //private static string BuildMethodMessage(MethodExecutionArgs args)
        //{
        //    var logDescription = args.Method.DeclaringType.FullName + "." + args.Method.Name + "(" + GetParameters(args) + ")";

        //    var logConfigAttribute = GetLogConfigAttribute(args);

        //    if (logConfigAttribute != null)
        //    {
        //        logDescription = logConfigAttribute.Description;
        //    }

        //    return logDescription;
        //}

        //private static LogConfigAttribute GetLogConfigAttribute(MethodExecutionArgs args)
        //{
        //    var logConfigAttribute = args.Method.GetCustomAttributes(typeof(LogConfigAttribute), false);

        //    if (logConfigAttribute.Length > 0)
        //    {
        //        return (LogConfigAttribute)logConfigAttribute[0];
        //    }

        //    return null;
        //}

        private static LogConfigAttribute GetLogConfigAttribute(ParameterInfo parameterInfo)
        {
            var logConfigAttribute = parameterInfo.GetCustomAttributes(typeof(LogConfigAttribute), false);

            if (logConfigAttribute.Length > 0)
            {
                return (LogConfigAttribute)logConfigAttribute[0];
            }

            return null;
        }

        //private static string GetParameters(MethodExecutionArgs args)
        //{
        //    var parameters = new List<string>();
        //    var excludeParameters = (from parameter in args.Method.GetParameters() where NoLog(parameter) select parameter.Position).ToList();

        //    for (var i = 0; i < args.Arguments.Count; i++)
        //    {
        //        var argument = args.Arguments.GetArgument(i);
        //        int value;

        //        if (argument == null)
        //        {
        //            parameters.Add("null");
        //        }
        //        else if (excludeParameters.Contains(i))
        //        {
        //            parameters.Add(new String('*', argument.ToString().Length));
        //        }
        //        else if (argument is string)
        //        {
        //            parameters.Add("\"" + argument + "\"");
        //        }
        //        else if (argument is bool)
        //        {
        //            parameters.Add(argument.ToString());
        //        }
        //        else if (!argument.GetType().IsValueType)
        //        {
        //            parameters.Add(argument.ToString());
        //        }
        //        else if (int.TryParse(argument.ToString(), out value))
        //        {
        //            parameters.Add(argument.ToString());
        //        }
        //        else
        //        {
        //            parameters.Add("\"" + argument + "\"");
        //        }
        //    }

        //    return string.Join(", ", parameters);
        //}
    }
}
