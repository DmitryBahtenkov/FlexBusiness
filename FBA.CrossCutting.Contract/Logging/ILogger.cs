using System;

namespace FBA.CrossCutting.Contract.Logging
{
    public interface ILogger
    {
        public void LogInfo(string message, params object[] parameters);
        public void LogInfo(Exception e, string message, params object[] parameters);
        
        public void LogError(string message, params object[] parameters);
        public void LogError(Exception e, string message, params object[] parameters);
        
        public void LogWarning(string message, params object[] parameters);
        public void LogWarning(Exception e, string message, params object[] parameters);
        
        public void LogTrace(string message, params object[] parameters);
        public void LogTrace(Exception e, string message, params object[] parameters);
        
        public void LogDebug(string message, params object[] parameters);
        public void LogDebug(Exception e, string message, params object[] parameters);
    }
}