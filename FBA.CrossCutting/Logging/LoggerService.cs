using System;
using Microsoft.Extensions.Logging;
using ILogger = FBA.CrossCutting.Contract.Logging.ILogger;

namespace FBA.CrossCutting.Logging
{
    public class LoggerService : ILogger
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        #region Info

        public void LogInfo(string message, params object[] parameters)
        {
            _logger.LogInformation(message, parameters);
        }

        public void LogInfo(Exception e, string message, params object[] parameters)
        {
            _logger.LogInformation(e, message, parameters);
        }


        #endregion

        #region Error

        public void LogError(string message, params object[] parameters)
        {
            _logger.LogError(message, parameters);
        }

        public void LogError(Exception e, string message, params object[] parameters)
        {
            _logger.LogError(e, message, parameters);
        }

        #endregion

        #region Warning

        public void LogWarning(string message, params object[] parameters)
        {
            _logger.LogWarning(message, parameters);
        }

        public void LogWarning(Exception e, string message, params object[] parameters)
        {
            _logger.LogWarning(e, message, parameters);
        }

        #endregion

        #region Trace

        public void LogTrace(string message, params object[] parameters)
        {
            _logger.LogTrace(message, parameters);
        }

        public void LogTrace(Exception e, string message, params object[] parameters)
        {
            _logger.LogTrace(e, message, parameters);
        }

        #endregion

        #region Debug

        public void LogDebug(string message, params object[] parameters)
        {
            _logger.LogDebug(message, parameters);
        }

        public void LogDebug(Exception e, string message, params object[] parameters)
        {
            _logger.LogDebug(e, message, parameters);
        }

        #endregion
    }
}