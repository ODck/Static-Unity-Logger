using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZLogger;

namespace Logging
{
    /// <summary>
    /// Static Logger to Log messages
    /// </summary>
    public static class Logger
    {
        private static ILogger _logger;

        private static ILogger CoreLogger
        {
            get
            {
                if (_logger != null)
                    return _logger;
                Init();
                return _logger;
            }
        }

        private static void Init()
        {
            _logger = UnityLoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddZLoggerUnityDebug();
            }).CreateLogger("Global");
        }

        /// <summary>
        /// Handles and Log the UnHandledExceptions thrown by the code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception) args.ExceptionObject;
            Error(e);
            //Exception is logged anyway without handling if the program is running in Debug Mode
        }

        #region LoggerMethods

        /// <summary>
        /// Format the string to show first the caller type
        /// </summary>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static string FormatForContext<T>(this string message)
        {
            return $"{typeof(T).Name} - {message}";
        }

        /// <summary>
        /// Format the string to show first the caller type
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string FormatForContext(this object message)
        {
            return $"{message.GetType().Name} - {message}";
        }

        /// <summary>
        /// Log a message if certain condition is not true
        /// </summary>
        /// <param name="assertion"></param>
        /// <param name="failedAssertionMessage"></param>
        public static void Assert(bool assertion, string failedAssertionMessage)
        {
            if (!assertion)
            {
                Error(failedAssertionMessage);
            }
        }

        /// <summary>
        /// Log a debug message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Debug(string message, params object[] args) => CoreLogger.ZLogDebug(message, args);
        
        /// <summary>
        /// Log a debug message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Debug<T>(string message, params object[] args) => CoreLogger.ZLogDebug(message.FormatForContext<T>(), args);

        /// <summary>
        /// Log a debug message 
        /// </summary>
        /// <param name="obj"></param>
        public static void Debug(object obj) => CoreLogger.ZLogDebug(obj.FormatForContext());

        /// <summary>
        /// Log a debug JsonConvert.SerializeObject() 
        /// </summary>
        /// <param name="obj"></param>
        public static void LogSerialized(object obj) => CoreLogger.ZLogDebug(JsonConvert.SerializeObject(obj));

        /// <summary>
        /// Log a info message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Info(string message, params object[] args) => CoreLogger.ZLogInformation(message, args);
        
        /// <summary>
        /// Log a info message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Info<T>(string message, params object[] args) => CoreLogger.ZLogInformation(message.FormatForContext<T>(), args);

        /// <summary>
        /// Log a info message 
        /// </summary>
        /// <param name="obj"></param>
        public static void Info(object obj) => CoreLogger.ZLogInformation(obj.FormatForContext());

        /// <summary>
        /// Log a warning message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Warning(string message, params object[] args) => CoreLogger.ZLogWarning(message, args);
        
        /// <summary>
        /// Log a warning message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Warning<T>(string message, params object[] args) => CoreLogger.ZLogWarning(message.FormatForContext<T>(), args);
        

        /// <summary>
        /// Log a warning message 
        /// </summary>
        /// <param name="obj"></param>
        public static void Warning(object obj) => CoreLogger.ZLogWarning(obj.FormatForContext());

        /// <summary>
        /// Log a error message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Error(string message, params object[] args) => CoreLogger.ZLogError(message, args);
        
        /// <summary>
        /// Log a error message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Error<T>(string message, params object[] args) => CoreLogger.ZLogError(message.FormatForContext<T>(), args);

        /// <summary>
        /// Log a error message 
        /// </summary>
        /// <param name="obj"></param>
        public static void Error(object obj) => CoreLogger.ZLogError(obj.FormatForContext());

        /// <summary>
        /// Log a error message 
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex) => CoreLogger.ZLogError(ex, ex.Message);

        #endregion
    }
}