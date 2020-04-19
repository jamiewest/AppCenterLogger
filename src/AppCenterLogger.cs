using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Microsoft.Extensions.Logging.AppCenter
{
    public class AppCenterLogger : ILogger
    {
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppCenterLogger"/> class.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        public AppCenterLogger(string name)
        {
            _name = name;
        }

        internal AppCenterLoggerOptions Options { get; set; }

        internal IExternalScopeProvider ScopeProvider { get; set; }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // If the filter is null, everything is enabled
            // unless the debugger is not attached
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);
            var properties = new Dictionary<string, string> { { nameof(message), message } };

            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                if (!(logLevel == LogLevel.Error))
                {
                    Analytics.TrackEvent(GetLogLevelString(logLevel), properties);
                }
                else
                {
                    Crashes.TrackError(exception);
                }
            }
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "Trace";
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Information:
                    return "Information";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Critical:
                    return "Critical";
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        
    }
}
