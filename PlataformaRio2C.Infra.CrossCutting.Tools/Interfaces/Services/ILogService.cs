using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces
{
    public interface ILogService
    {
        void Log(LogType type, string format, params object[] args);
        void Log(LogType type, string messageFormated);
        void Log(LogType type, string messageFormated, Exception innerException);
    }
}
