using System;
using System.Diagnostics;

namespace CodeSandbox.SDK.Net.Internal
{
    public enum LogLevel
    {
        Trace = 0,
        Info = 1,
        Success = 2,
        Warning = 3,
        Error = 4
    }

    public class LoggerService
    {
        private readonly Action<string> _logAction;
        private readonly LogLevel _minimumLevel;

        public LoggerService(LogLevel minimumLevel = LogLevel.Info)
        {
            _logAction = LogToConsoleAndDebug;
            _minimumLevel = minimumLevel;
        }

        public LoggerService(Action<string> customLogAction, LogLevel minimumLevel = LogLevel.Info)
        {
            _logAction = customLogAction ?? LogToConsoleAndDebug;
            _minimumLevel = minimumLevel;
        }

        private void LogToConsoleAndDebug(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        private void Log(LogLevel level, string message)
        {
#if DEBUG 
            _logAction?.Invoke($"[{level.ToString().ToUpper()}] {DateTime.Now:O} - {message}");
#else
            // In release mode, filter by minimum level
            if (level >= _minimumLevel)
            {
                _logAction?.Invoke($"[{level.ToString().ToUpper()}] {DateTime.Now:O} - {message}");
            }
#endif
        }

        public void LogTrace(string message)
        {
            Log(LogLevel.Trace, message);
        }

        public void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void LogSuccess(string message)
        {
            Log(LogLevel.Success, message);
        }

        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        public void LogError(string message, Exception ex)
        {
            string fullMessage = $"{message}\nException: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}";
            Log(LogLevel.Error, fullMessage);
        }
    }
}
