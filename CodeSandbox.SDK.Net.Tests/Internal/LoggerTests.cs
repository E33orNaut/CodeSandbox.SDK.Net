using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CodeSandbox.SDK.Net.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeSandbox.SDK.Net.Tests.Internal
{
    [TestClass]
    public class LoggerServiceTests
    {
        private readonly StringBuilder _logOutput;
        private readonly LoggerService _logger;

        public LoggerServiceTests()
        {
            _logOutput = new StringBuilder();
            _logger = new LoggerService(msg => _logOutput.AppendLine(msg), LogLevel.Trace);
        }

        [TestInitialize]
        public void Setup()
        {
            _ = _logOutput.Clear();
        }

        [TestMethod]
        public void LogTrace_Message_ShouldBeLogged()
        {
            _logger.LogTrace("Trace message");
            StringAssert.Contains(_logOutput.ToString(), "TRACE");
            StringAssert.Contains(_logOutput.ToString(), "Trace message");
        }

        [TestMethod]
        public void LogInfo_Message_ShouldBeLogged()
        {
            _logger.LogInfo("Info message");
            StringAssert.Contains(_logOutput.ToString(), "INFO");
            StringAssert.Contains(_logOutput.ToString(), "Info message");
        }

        [TestMethod]
        public void LogSuccess_Message_ShouldBeLogged()
        {
            _logger.LogSuccess("Success message");
            StringAssert.Contains(_logOutput.ToString(), "SUCCESS");
            StringAssert.Contains(_logOutput.ToString(), "Success message");
        }

        [TestMethod]
        public void LogWarning_Message_ShouldBeLogged()
        {
            _logger.LogWarning("Warning message");
            StringAssert.Contains(_logOutput.ToString(), "WARNING");
            StringAssert.Contains(_logOutput.ToString(), "Warning message");
        }

        [TestMethod]
        public void LogError_Message_ShouldBeLogged()
        {
            _logger.LogError("Error message");
            StringAssert.Contains(_logOutput.ToString(), "ERROR");
            StringAssert.Contains(_logOutput.ToString(), "Error message");
        }

        [TestMethod]
        public void LogError_WithException_ShouldIncludeDetails()
        {
            InvalidOperationException ex = new InvalidOperationException("Invalid operation");
            _logger.LogError("Operation failed", ex);

            string log = _logOutput.ToString();
            StringAssert.Contains(log, "ERROR");
            StringAssert.Contains(log, "Operation failed");
            StringAssert.Contains(log, "InvalidOperationException");
            StringAssert.Contains(log, "Invalid operation");
        }


        [TestMethod]
        public void LoggerService_UsesDefaultConsoleAndDebug_IfNullCustomActionProvided()
        {
            LoggerService defaultLogger = new LoggerService(null);
            defaultLogger.LogSuccess("Test message");
            // Not testable directly, but should not throw.
        }

        [TestMethod]
        public void LoggerService_ConcurrentLogging_ShouldRemainStable()
        {
            ConcurrentQueue<string> concurrentOutput = new ConcurrentQueue<string>();
            LoggerService concurrentLogger = new LoggerService(msg => concurrentOutput.Enqueue(msg), LogLevel.Trace);

            _ = Parallel.For(0, 1000, i =>
            {
                concurrentLogger.LogInfo($"Message {i}");
            });

            Assert.AreEqual(1000, concurrentOutput.Count);
            foreach (string message in concurrentOutput)
            {
                StringAssert.Contains(message, "INFO");
            }
        }

        [TestMethod]
        public void LoggerService_PerformanceBenchmark_ShouldLogUnderThreshold()
        {
            LoggerService perfLogger = new LoggerService(msg => { }, LogLevel.Trace);
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                perfLogger.LogTrace("Benchmark test message");
            }

            stopwatch.Stop();

            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 1000, $"Logging took too long: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
