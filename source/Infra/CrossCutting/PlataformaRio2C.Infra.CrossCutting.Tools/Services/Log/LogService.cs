using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using log4net;
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Services.Log
{
    public class LogService : ILogService
    {
        private static readonly ILog _loggerObj = LogManager.GetLogger("root");
        private bool _enableEncryption = false;

        public LogService() { }

        public LogService(bool enableEncryption)
        {
            _enableEncryption = enableEncryption;
        }

        /// <summary>
        /// Enable or not encryption for log messages
        /// </summary>
        public bool EnableEncryption { set { _enableEncryption = value; } }

        /// <summary>
        /// Método que escreve no arquivo de log
        /// </summary>
        /// <param name="type">Tipo de log - Enum de tipos.</param>
        /// <param name="format">Template string com parametros de substituição para o string de mensagem.</param>
        /// <param name="args">Argumentos a serem substituidos nos parametros do template.</param>
        /// <example>Log(LogType.INFO, "Ocorreu um erro ao salvar o {0} de nome {1}", "usuário", "João");</example>
        public void Log(LogType type, string format, params object[] args)
        {
            Log(type, string.Format(format, args), exception: null);
        }

        /// <summary>
        /// Método que escreve no arquivo de log
        /// </summary>
        /// <param name="type">Tipo de log - Enum de tipos.</param>
        /// <param name="messageFormated">Texto a ser salvo no log.</param>
        /// <example>Log(LogType.ERROR, "Ocorreu um erro ao salvar o usuário de nome João");</example>
        public void Log(LogType type, string messageFormated)
        {
            Log(type, messageFormated, exception: null);
        }

        /// <summary>
        /// Método que escreve no arquivo de log
        /// </summary>
        /// <param name="type">Tipo de log - Enum de tipos.</param>
        /// <param name="messageFormated">Texto a ser salvo no log.</param>
        /// <param name="innerException">Excetion a ser logada.</param>
        /// <example>Log(LogType.ERROR, "Ocorreu um erro ao salvar o usuário de nome João", ex.InnerException);</example>
        public void Log(LogType type, string messageFormated, Exception exception)
        {
            if (_enableEncryption)
            {
                // messageFormated = messageFormated.Encrypt();
            }

            switch (type)
            {
                case LogType.INFO:
                    {
                        LogInfo(messageFormated, exception);
                        break;
                    }
                case LogType.DEBUG:
                    {
                        LogDebug(messageFormated, exception);
                        break;
                    }
                case LogType.ERROR:
                    {
                        LogError(messageFormated, exception);
                        break;
                    }
                case LogType.WARNING:
                    {
                        LogWarn(messageFormated, exception);
                        break;
                    }
                case LogType.FATAL:
                    {
                        LogFatal(messageFormated, exception);
                        break;
                    }
            }
        }

        #region Private methods

        private static void ConfigureLogger()
        {
            if (!_loggerObj.Logger.Repository.Configured)
            {
                GlobalContext.Properties["MachineName"] = Environment.MachineName;
                log4net.Config.XmlConfigurator.Configure();
            }
        }

        private void LogInfo(string message, Exception inner)
        {
            ConfigureLogger();
            if (inner == null)
                _loggerObj.Info(message);
            else
                _loggerObj.Info(message, inner);
        }

        private void LogDebug(string message, Exception inner)
        {
            ConfigureLogger();
            if (inner == null)
                _loggerObj.Debug(message);
            else
                _loggerObj.Debug(message, inner);
        }

        private void LogError(string message, Exception inner)
        {
            ConfigureLogger();
            if (inner == null)
                _loggerObj.Error(message);
            else
                _loggerObj.Error(message, inner);
        }

        private void LogWarn(string message, Exception inner)
        {
            ConfigureLogger();
            if (inner == null)
                _loggerObj.Warn(message);
            else
                _loggerObj.Warn(message, inner);
        }

        private void LogFatal(string message, Exception inner)
        {
            ConfigureLogger();
            if (inner == null)
                _loggerObj.Fatal(message);
            else
                _loggerObj.Fatal(message, inner);
        }

        #endregion
    }
}
