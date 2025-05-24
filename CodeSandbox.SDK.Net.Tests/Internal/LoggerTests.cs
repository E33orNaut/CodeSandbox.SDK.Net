using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CodeSandbox.SDK.Net.Internal;

namespace CodeSandbox.SDK.Net.Tests.Internal
{
    [TestClass]
    public class LoggerServiceTests
    {
        private List<string> _loggedMessages;
        private LoggerService _logger;

        [TestInitialize]
        public void Setup()
        {
            _loggedMessages = new List<string>();
            _logger = new LoggerService(msg => _loggedMessages.Add(msg), LogLevel.Trace);
        }

        [TestMethod]
        public void LogTrace_MessageLogged_WhenLevelIsTrace()
        {
            _logger.LogTrace("Trace message");
            Assert.IsTrue(_loggedMessages.Count == 1);
            StringAssert.Contains(_loggedMessages[0], "TRACE");
            StringAssert.Contains(_loggedMessages[0], "Trace message");
        }

        [TestMethod]
        public void LogInfo_MessageLogged_WhenLevelIsInfo()
        {
            _logger.LogInfo("Info message");
            Assert.IsTrue(_loggedMessages.Count == 1);
            StringAssert.Contains(_loggedMessages[0], "INFO");
            StringAssert.Contains(_loggedMessages[0], "Info message");
        }

        [TestMethod]
        public void LogSuccess_MessageLogged_WhenLevelIsSuccess()
        {
            _logger.LogSuccess("Success message");
            Assert.IsTrue(_loggedMessages.Count == 1);
            StringAssert.Contains(_loggedMessages[0], "SUCCESS");
            StringAssert.Contains(_loggedMessages[0], "Success message");
        }

        [TestMethod]
        public void LogWarning_MessageLogged_WhenLevelIsWarning()
        {
            _logger.LogWarning("Warning message");
            Assert.IsTrue(_loggedMessages.Count == 1);
            StringAssert.Contains(_loggedMessages[0], "WARNING");
            StringAssert.Contains(_loggedMessages[0], "Warning message");
        }

        [TestMethod]
        public void LogError_MessageLogged_WhenLevelIsError()
        {
            _logger.LogError("Error message");
            Assert.IsTrue(_loggedMessages.Count == 1);
            StringAssert.Contains(_loggedMessages[0], "ERROR");
            StringAssert.Contains(_loggedMessages[0], "Error message");
        }

        [TestMethod]
        public void LogError_WithException_LogsExceptionDetails()
        {
            var ex = new InvalidOperationException("Invalid operation");
            _logger.LogError("Exception occurred", ex);

            Assert.IsTrue(_loggedMessages.Count == 1);
            StringAssert.Contains(_loggedMessages[0], "ERROR");
            StringAssert.Contains(_loggedMessages[0], "Exception occurred");
            StringAssert.Contains(_loggedMessages[0], "InvalidOperationException");
            StringAssert.Contains(_loggedMessages[0], "Invalid operation");
            StringAssert.Contains(_loggedMessages[0], "StackTrace");
        }

        [TestMethod]
        public void Log_WithHigherMinimumLevel_IgnoresLowerSeverityLogs()
        {
            _logger = new LoggerService(msg => _loggedMessages.Add(msg), LogLevel.Warning);
            _logger.LogTrace("Should not log");
            _logger.LogInfo("Should not log");
            _logger.LogSuccess("Should not log");

            Assert.AreEqual(0, _loggedMessages.Count);
        }

        [TestMethod]
        public void CustomLogAction_Null_DefaultsToConsoleAndDebug()
        {
            var logger = new LoggerService(null, LogLevel.Info);
            Assert.IsNotNull(logger);
            // Cannot test Console/Debug output easily, but this verifies instantiation does not fail
        }

        [TestMethod]
        public void Timestamp_IsFormattedCorrectly()
        {
            _logger.LogInfo("Timestamp check");
            Assert.IsTrue(_loggedMessages[0].Contains("T") && _loggedMessages[0].Contains("-"));
        }

        [TestMethod]
        public void MessagePrefix_CorrectlyFormatted()
        {
            _logger.LogWarning("Prefix check");
            StringAssert.StartsWith(_loggedMessages[0], "[WARNING]");
        }
    }
}
