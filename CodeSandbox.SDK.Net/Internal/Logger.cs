using System;
using System.Diagnostics;

namespace CodeSandbox.SDK.Net.Internal
{
    /// <summary>
    /// Represents the severity level of a log message.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Detailed tracing information.
        /// </summary>
        Trace = 0,

        /// <summary>
        /// General informational messages.
        /// </summary>
        Info = 1,

        /// <summary>
        /// Successful operation messages.
        /// </summary>
        Success = 2,

        /// <summary>
        /// Warning messages indicating potential issues.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Error messages indicating failures.
        /// </summary>
        Error = 4
    }

    /// <summary>
    /// Provides logging capabilities with configurable minimum log level and optional custom log output.
    /// </summary>
    public class LoggerService
    {
        private readonly Action<string> _logAction;
        private readonly LogLevel _minimumLevel;

        /// <summary>
        /// Initializes a new instance of <see cref="LoggerService"/> with the specified minimum log level.
        /// Logs are written to the console and Debug output by default.
        /// </summary>
        /// <param name="minimumLevel">The minimum <see cref="LogLevel"/> to log. Defaults to <see cref="LogLevel.Info"/>.</param>
        public LoggerService(LogLevel minimumLevel = LogLevel.Info)
        {
            _logAction = LogToConsoleAndDebug;
            _minimumLevel = minimumLevel;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LoggerService"/> with a custom log action and minimum log level.
        /// </summary>
        /// <param name="customLogAction">A custom action to handle log messages. If null, defaults to logging to console and Debug output.</param>
        /// <param name="minimumLevel">The minimum <see cref="LogLevel"/> to log. Defaults to <see cref="LogLevel.Info"/>.</param>
        public LoggerService(Action<string> customLogAction, LogLevel minimumLevel = LogLevel.Info)
        {
            _logAction = customLogAction ?? LogToConsoleAndDebug;
            _minimumLevel = minimumLevel;
        }

        /// <summary>
        /// Logs the specified message to both the console and the debug output.
        /// </summary>
        /// <param name="message">The message to log.</param>
        private void LogToConsoleAndDebug(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        /// <summary>
        /// Logs a message with the specified <see cref="LogLevel"/> if it meets the minimum level criteria.
        /// In DEBUG builds, all messages are logged regardless of level.
        /// </summary>
        /// <param name="level">The severity level of the log message.</param>
        /// <param name="message">The message to log.</param>
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

        /// <summary>
        /// Logs a message with the <see cref="LogLevel.Trace"/> level.
        /// </summary>
        /// <param name="message">The trace message to log.</param>
        public void LogTrace(string message)
        {
            Log(LogLevel.Trace, message);
        }

        /// <summary>
        /// Logs a message with the <see cref="LogLevel.Info"/> level.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        public void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }

        /// <summary>
        /// Logs a message with the <see cref="LogLevel.Success"/> level.
        /// </summary>
        /// <param name="message">The success message to log.</param>
        public void LogSuccess(string message)
        {
            Log(LogLevel.Success, message);
        }

        /// <summary>
        /// Logs a message with the <see cref="LogLevel.Warning"/> level.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        /// <summary>
        /// Logs a message with the <see cref="LogLevel.Error"/> level.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Logs an error message along with detailed exception information.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="ex">The exception to include in the log.</param>
        public void LogError(string message, Exception ex)
        {
            string fullMessage = $"{message}\nException: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}";
            Log(LogLevel.Error, fullMessage);
        }
    }
}
